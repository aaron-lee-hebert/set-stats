# SetStats User Flow Diagrams

This document contains user flow diagrams for the SetStats application using Mermaid syntax.

## 1. Registration & Authentication Flow

```mermaid
flowchart TD
    Start([User Visits Site]) --> CheckAuth{Authenticated?}
    CheckAuth -->|Yes| Dashboard[Dashboard]
    CheckAuth -->|No| Landing[Landing Page]

    Landing --> Login[Click Login]
    Landing --> Register[Click Register]

    Login --> EnterCreds[Enter Email & Password]
    EnterCreds --> ValidateCreds{Valid Credentials?}
    ValidateCreds -->|Yes| Dashboard
    ValidateCreds -->|No| LoginError[Show Error Message]
    LoginError --> EnterCreds

    Register --> EnterInvite[Enter Invitation Code]
    EnterInvite --> ValidateInvite{Valid Invitation?}
    ValidateInvite -->|No| InviteError[Show Error: Invalid or Expired Code]
    InviteError --> EnterInvite
    ValidateInvite -->|Yes| RegistrationForm[Fill Registration Form<br/>Email, Password, Name]
    RegistrationForm --> SubmitReg[Submit Registration]
    SubmitReg --> ValidateReg{Validation Passed?}
    ValidateReg -->|No| RegError[Show Validation Errors]
    RegError --> RegistrationForm
    ValidateReg -->|Yes| CreateAccount[Create Account<br/>Mark Invitation as Used]
    CreateAccount --> ConfirmEmail{Email Confirmation<br/>Required?}
    ConfirmEmail -->|Yes| EmailSent[Send Confirmation Email]
    EmailSent --> WaitConfirm[Wait for Email Confirmation]
    WaitConfirm --> Dashboard
    ConfirmEmail -->|No| Dashboard
```

## 2. Admin - User Invitation Flow

```mermaid
flowchart TD
    Start([Admin Logs In]) --> AdminDash[Admin Dashboard]
    AdminDash --> ManageInvites[Click Manage Invitations]

    ManageInvites --> InviteList[View Active Invitations]
    InviteList --> CreateNew[Click Create New Invitation]
    InviteList --> ViewUsed[View Used Invitations]
    InviteList --> DeleteInvite[Delete Unused Invitation]

    CreateNew --> EnterEmail[Enter Invitee Email<br/>Optional]
    EnterEmail --> SetExpiration[Set Expiration Date<br/>Default: 7 days]
    SetExpiration --> GenerateCode[Generate Unique Code]
    GenerateCode --> SaveInvite[Save to Database]
    SaveInvite --> DisplayCode[Display Invitation Code<br/>& Shareable Link]
    DisplayCode --> SendEmail{Send Email?}
    SendEmail -->|Yes| EmailInvite[Email Invitation to User]
    SendEmail -->|No| CopyCode[Copy Code Manually]
    EmailInvite --> InviteList
    CopyCode --> InviteList
```

## 3. Exercise Management Flow

```mermaid
flowchart TD
    Start([User in Dashboard]) --> ExerciseMenu[Navigate to Exercises]

    ExerciseMenu --> ExerciseList[View Exercise List]
    ExerciseList --> Filter[Filter/Search Exercises]
    ExerciseList --> AddNew[Click Add Exercise]
    ExerciseList --> EditExist[Click Edit Exercise]
    ExerciseList --> DeleteEx[Click Delete Exercise]
    ExerciseList --> ViewMax[View/Edit 1RM]

    AddNew --> ExForm[Fill Exercise Form<br/>Name, Category, Description]
    ExForm --> SaveEx[Save Exercise]
    SaveEx --> ExerciseList

    EditExist --> ExEditForm[Edit Exercise Details]
    ExEditForm --> UpdateEx[Update Exercise]
    UpdateEx --> ExerciseList

    DeleteEx --> ConfirmDelete{Confirm Deletion?}
    ConfirmDelete -->|Yes| RemoveEx[Delete Exercise]
    ConfirmDelete -->|No| ExerciseList
    RemoveEx --> ExerciseList

    ViewMax --> MaxList[View All 1RMs]
    MaxList --> SetMax[Set/Update 1RM<br/>for Exercise]
    SetMax --> Calculate{Auto-Calculate<br/>from Recent Sets?}
    Calculate -->|Yes| UseFormula[Use Epley/Brzycki Formula]
    Calculate -->|No| ManualEntry[Enter 1RM Manually]
    UseFormula --> SaveMax[Save 1RM]
    ManualEntry --> SaveMax
    SaveMax --> MaxList
```

## 4. Training Program Setup Flow

```mermaid
flowchart TD
    Start([User in Dashboard]) --> Programs[Navigate to Programs]

    Programs --> ProgramList[View Training Programs]
    ProgramList --> UseTemplate[Use StrongLifts Template]
    ProgramList --> CreateCustom[Create Custom Program]
    ProgramList --> EditProgram[Edit Existing Program]
    ProgramList --> ActivateProgram[Activate Program]

    UseTemplate --> LoadTemplate[Load StrongLifts 5x5 Template]
    LoadTemplate --> CustomizeTemplate[Customize if Needed]
    CustomizeTemplate --> SaveProgram[Save Program]

    CreateCustom --> EnterProgramDetails[Enter Program Name<br/>Description, Goal]
    EnterProgramDetails --> DefineWeeks[Define Week Structure<br/>Standard/Deload Weeks]
    DefineWeeks --> DefineDays[Define Workout Days<br/>Per Week]
    DefineDays --> AddExercises[Add Exercises to Each Day]
    AddExercises --> SetParameters[Set Training Parameters<br/>% of 1RM, Reps, AMRAP]
    SetParameters --> SaveProgram
    SaveProgram --> ProgramList

    EditProgram --> ModifyProgram[Modify Program Details]
    ModifyProgram --> UpdateProgram[Update Program]
    UpdateProgram --> ProgramList

    ActivateProgram --> CreateCycle[Create Training Cycle]
    CreateCycle --> SetStartDate[Set Cycle Start Date]
    SetStartDate --> SetRounding[Set Weight Rounding<br/>Default: 2.5 lbs]
    SetRounding --> GenerateSchedule[Auto-Generate<br/>Workout Schedule]
    GenerateSchedule --> CycleActive[Program Active]
    CycleActive --> Dashboard
```

## 5. Active Workout Session Flow

```mermaid
flowchart TD
    Start([User in Dashboard]) --> ViewToday[View Today's Workout]
    ViewToday --> CheckScheduled{Workout<br/>Scheduled?}

    CheckScheduled -->|No| NoWorkout[No Workout Today<br/>Rest Day]
    CheckScheduled -->|Yes| WorkoutDetails[Show Workout Details<br/>Exercises & Sets]

    WorkoutDetails --> StartWorkout[Click Start Workout]
    StartWorkout --> LoadFirst[Load First Exercise & Set]

    LoadFirst --> ShowSet[Display:<br/>- Exercise Name<br/>- Target Weight & Reps<br/>- Plate Calculator<br/>- Rest Timer]

    ShowSet --> PlateCalc[Use Plate Calculator]
    PlateCalc --> ShowPlates[Display Plates Per Side]
    ShowPlates --> PerformSet[Perform Physical Set]

    PerformSet --> LogSet[Log Set Results]
    LogSet --> EnterWeight[Enter Actual Weight]
    EnterWeight --> EnterReps[Enter Actual Reps]
    EnterReps --> EnterRPE[Enter RPE 1-10<br/>Optional]
    EnterRPE --> AddNotes[Add Notes<br/>Optional]
    AddNotes --> SaveSet[Save Completed Set]

    SaveSet --> MoreSets{More Sets<br/>Remaining?}
    MoreSets -->|Yes| StartTimer[Start Rest Timer]
    StartTimer --> WaitRest[Wait for Rest Period]
    WaitRest --> NextSet[Load Next Set]
    NextSet --> ShowSet

    MoreSets -->|No| MoreExercises{More Exercises?}
    MoreExercises -->|Yes| LoadNextEx[Load Next Exercise]
    LoadNextEx --> ShowSet

    MoreExercises -->|No| WorkoutComplete[Workout Complete!]
    WorkoutComplete --> UpdateStatus[Update WorkoutDay Status<br/>to Completed]
    UpdateStatus --> ShowSummary[Show Workout Summary<br/>Total Volume, PRs, etc.]
    ShowSummary --> UpdateMax{New PR or<br/>Update 1RM?}
    UpdateMax -->|Yes| RecalcMax[Recalculate E1RM]
    UpdateMax -->|No| Dashboard
    RecalcMax --> Dashboard

    ShowSet --> SkipSet[Skip Set]
    SkipSet --> MoreSets
```

## 6. Progress Tracking & Reporting Flow

```mermaid
flowchart TD
    Start([User in Dashboard]) --> Reports[Navigate to Progress/Reports]

    Reports --> SelectView{Select View Type}

    SelectView --> ExProgress[Exercise Progress]
    SelectView --> E1RMChart[E1RM Over Time]
    SelectView --> VolumeChart[Training Volume]
    SelectView --> PRRecords[Personal Records]
    SelectView --> Consistency[Training Consistency]

    ExProgress --> SelectExercise[Select Exercise]
    SelectExercise --> SelectDateRange[Select Date Range]
    SelectDateRange --> ShowWeightChart[Show Weight Progression<br/>Line Chart]
    ShowWeightChart --> ShowRepsChart[Show Reps Over Time]
    ShowRepsChart --> ExportData[Export to CSV]

    E1RMChart --> SelectExerciseE1RM[Select Exercise]
    SelectExerciseE1RM --> SelectDateRangeE1RM[Select Date Range]
    SelectDateRangeE1RM --> CalculateE1RM[Calculate E1RM<br/>from Completed Sets]
    CalculateE1RM --> DisplayE1RMChart[Display E1RM Trend Chart]
    DisplayE1RMChart --> ShowMaxLine[Overlay Actual 1RM]
    ShowMaxLine --> ExportData

    VolumeChart --> SelectPeriod[Select Time Period<br/>Weekly/Monthly]
    SelectPeriod --> CalcVolume[Calculate Total Volume<br/>Sets × Reps × Weight]
    CalcVolume --> DisplayVolumeChart[Display Volume Chart<br/>Bar Chart by Period]
    DisplayVolumeChart --> ShowAvgRPE[Show Average RPE]
    ShowAvgRPE --> ExportData

    PRRecords --> ShowAllPRs[Display All Personal Records]
    ShowAllPRs --> FilterByExercise[Filter by Exercise]
    FilterByExercise --> ShowPRHistory[Show PR History<br/>Date Achieved, Weight, Reps]
    ShowPRHistory --> ExportData

    Consistency --> CalcStats[Calculate Training Stats<br/>Completed vs Planned<br/>Workout Frequency]
    CalcStats --> DisplayConsistency[Display Consistency Dashboard<br/>Heatmap Calendar]
    DisplayConsistency --> ShowStreak[Show Current Streak]
    ShowStreak --> IdentifyMissed[Identify Missed Workouts]
    IdentifyMissed --> ExportData

    ExportData --> BackToReports[Back to Reports Menu]
    BackToReports --> Reports
```

## 7. Plate Calculator Utility Flow

```mermaid
flowchart TD
    Start([User Needs Plate Calculation]) --> Access{Access Method}

    Access -->|During Workout| InlineCalc[Inline in Workout View]
    Access -->|Standalone| UtilityPage[Navigate to Plate Calculator]

    InlineCalc --> AutoLoad[Auto-Load Target Weight<br/>from Programmed Set]
    UtilityPage --> ManualEntry[Manually Enter Target Weight]

    AutoLoad --> ShowBarWeight[Display Barbell Weight<br/>Default: 45 lbs]
    ManualEntry --> ShowBarWeight

    ShowBarWeight --> AdjustBar{Change Bar Weight?}
    AdjustBar -->|Yes| SelectBar[Select Different Bar<br/>45/35/25/20 kg or lbs]
    AdjustBar -->|No| SelectPlates[Show Available Plates<br/>from User Settings]
    SelectBar --> SelectPlates

    SelectPlates --> Calculate[Calculate Optimal<br/>Plate Combination]
    Calculate --> CheckValid{Valid<br/>Combination?}

    CheckValid -->|No| ShowError[Cannot Load Exactly<br/>Show Closest Alternative]
    ShowError --> DisplayPlates
    CheckValid -->|Yes| DisplayPlates[Display Plates Per Side<br/>Visual Representation]

    DisplayPlates --> ShowOrder[Show Loading Order<br/>Innermost to Outermost]
    ShowOrder --> VerifyTotal[Verify Total Weight]
    VerifyTotal --> UseInWorkout{In Workout Session?}

    UseInWorkout -->|Yes| ReturnToSet[Return to Set Logging]
    UseInWorkout -->|No| NewCalc[Calculate Another Weight]
    NewCalc --> ManualEntry

    ReturnToSet --> ContinueWorkout[Continue Workout]
```

## 8. Workout History & Review Flow

```mermaid
flowchart TD
    Start([User in Dashboard]) --> History[Navigate to Workout History]

    History --> CalendarView[View Workout Calendar]
    CalendarView --> SelectMonth[Select Month/Year]
    SelectMonth --> ShowCalendar[Display Calendar<br/>Highlight Completed Days]

    ShowCalendar --> ClickDay[Click on Workout Day]
    ClickDay --> WorkoutDetail[Show Workout Details]

    WorkoutDetail --> DisplayInfo[Display:<br/>- Date & Duration<br/>- Exercises Performed<br/>- All Sets with Weight/Reps/RPE<br/>- Workout Notes<br/>- Total Volume]

    DisplayInfo --> Actions{Select Action}

    Actions --> EditWorkout[Edit Workout]
    Actions --> DeleteWorkout[Delete Workout]
    Actions --> ViewExercise[View Exercise History]
    Actions --> CompareWorkout[Compare to Previous Workout]
    Actions --> BackToCalendar[Back to Calendar]

    EditWorkout --> ModifySet[Modify Set Data<br/>Weight/Reps/RPE/Notes]
    ModifySet --> SaveChanges[Save Changes]
    SaveChanges --> RecalcStats[Recalculate Statistics<br/>E1RM, PRs, Volume]
    RecalcStats --> WorkoutDetail

    DeleteWorkout --> ConfirmDelete{Confirm Deletion?}
    ConfirmDelete -->|Yes| RemoveWorkout[Delete Workout & Sets]
    ConfirmDelete -->|No| WorkoutDetail
    RemoveWorkout --> RecalcStats

    ViewExercise --> ExerciseHistory[Show All Instances<br/>of This Exercise]
    ExerciseHistory --> ProgressChart[Display Progress Chart]
    ProgressChart --> WorkoutDetail

    CompareWorkout --> FindPrevious[Find Previous Similar Workout]
    FindPrevious --> ShowComparison[Side-by-Side Comparison<br/>Highlight Improvements]
    ShowComparison --> WorkoutDetail

    BackToCalendar --> ShowCalendar
```

## 9. User Settings & Profile Flow

```mermaid
flowchart TD
    Start([User in Dashboard]) --> Profile[Navigate to Profile/Settings]

    Profile --> ViewProfile[View Profile Information]
    ViewProfile --> SelectSection{Select Section}

    SelectSection --> PersonalInfo[Personal Information]
    SelectSection --> Preferences[App Preferences]
    SelectSection --> Security[Security Settings]
    SelectSection --> Equipment[Equipment Settings]

    PersonalInfo --> EditName[Edit First/Last Name]
    EditName --> UpdateEmail[Update Email]
    UpdateEmail --> SavePersonal[Save Changes]
    SavePersonal --> ViewProfile

    Preferences --> UnitSystem[Select Unit System<br/>Imperial lbs / Metric kg]
    UnitSystem --> RoundingFactor[Set Default Rounding Factor<br/>2.5 / 5 / 10 lbs]
    RoundingFactor --> RestTime[Set Default Rest Times<br/>by Exercise Category]
    RestTime --> Notifications[Configure Notifications]
    Notifications --> SavePreferences[Save Preferences]
    SavePreferences --> ViewProfile

    Security --> ChangePassword[Change Password]
    Security --> Enable2FA[Enable Two-Factor Auth<br/>Optional]
    Security --> ViewSessions[View Active Sessions]

    ChangePassword --> EnterOldPass[Enter Current Password]
    EnterOldPass --> EnterNewPass[Enter New Password]
    EnterNewPass --> ConfirmNewPass[Confirm New Password]
    ConfirmNewPass --> ValidatePass{Valid?}
    ValidatePass -->|No| PassError[Show Error]
    PassError --> EnterNewPass
    ValidatePass -->|Yes| UpdatePassword[Update Password]
    UpdatePassword --> ViewProfile

    Equipment --> AvailablePlates[Configure Available Plates]
    AvailablePlates --> SelectPlateSet[Select Plate Set<br/>Standard/Olympic/Custom]
    SelectPlateSet --> AddRemovePlates[Add/Remove Individual Plates]
    AddRemovePlates --> SetBars[Configure Available Barbells<br/>45/35/20 kg, etc.]
    SetBars --> SaveEquipment[Save Equipment Config]
    SaveEquipment --> ViewProfile
```

## 10. Admin Dashboard Flow

```mermaid
flowchart TD
    Start([Admin Logs In]) --> AdminDash[Admin Dashboard]

    AdminDash --> Overview[View System Overview<br/>Total Users, Active Cycles, etc.]
    Overview --> SelectAdmin{Select Admin Function}

    SelectAdmin --> ManageUsers[Manage Users]
    SelectAdmin --> ManageInvites[Manage Invitations]
    SelectAdmin --> SystemSettings[System Settings]
    SelectAdmin --> ViewLogs[View System Logs]

    ManageUsers --> UserList[View All Users]
    UserList --> UserActions{Select Action}

    UserActions --> ViewUser[View User Details]
    UserActions --> EditUser[Edit User Info]
    UserActions --> DeactivateUser[Deactivate User]
    UserActions --> DeleteUser[Delete User]

    ViewUser --> UserStats[View User Statistics<br/>Workouts, Cycles, Activity]
    UserStats --> UserList

    EditUser --> ModifyUser[Modify User Info/Roles]
    ModifyUser --> SaveUser[Save User Changes]
    SaveUser --> UserList

    DeactivateUser --> ConfirmDeactivate{Confirm?}
    ConfirmDeactivate -->|Yes| DisableAccount[Disable User Account]
    ConfirmDeactivate -->|No| UserList
    DisableAccount --> UserList

    DeleteUser --> ConfirmDelete{Confirm Deletion?<br/>Permanent Action}
    ConfirmDelete -->|Yes| RemoveUser[Delete User & All Data]
    ConfirmDelete -->|No| UserList
    RemoveUser --> UserList

    ManageInvites --> InviteFlow[See Admin Invitation Flow]
    SystemSettings --> ConfigApp[Configure App Settings]
    ViewLogs --> LogsList[View Application Logs<br/>Errors, Auth, Activity]
```

---

## Navigation Map

```mermaid
graph TD
    Landing[Landing Page] --> Login[Login]
    Landing --> Register[Register]

    Login --> Dashboard[Dashboard]
    Register --> Dashboard

    Dashboard --> TodayWorkout[Today's Workout]
    Dashboard --> Programs[Training Programs]
    Dashboard --> Exercises[Exercise Management]
    Dashboard --> History[Workout History]
    Dashboard --> Reports[Progress Reports]
    Dashboard --> PlateCalc[Plate Calculator]
    Dashboard --> Profile[Profile & Settings]

    Dashboard --> Admin[Admin Dashboard<br/>Admin Only]

    TodayWorkout --> ActiveWorkout[Active Workout Session]
    Programs --> ProgramManage[Create/Edit Programs]
    Exercises --> ExerciseList[Exercise List]
    Exercises --> OneRepMax[1RM Management]
    History --> Calendar[Workout Calendar]
    Reports --> Charts[Charts & Analytics]
    PlateCalc --> Calculator[Plate Calculator Utility]
    Profile --> Settings[Settings]

    Admin --> AdminUsers[User Management]
    Admin --> AdminInvites[Invitation Management]
    Admin --> AdminSystem[System Settings]
```

---

## Notes

- All diagrams use Mermaid syntax and can be rendered in GitHub, VS Code, or any Mermaid-compatible viewer
- Flows assume authenticated users unless otherwise specified
- Admin flows are restricted to users with Admin role
- Error handling and validation steps are included throughout
- Mobile-responsive design considerations should be applied to all flows
