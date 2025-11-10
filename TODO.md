# SetStats Development Roadmap

This document outlines the development plan to transform SetStats into a production-ready personal weightlifting app inspired by StrongLifts, deployed on Fly.io.

## Project Vision

A personal weightlifting tracking application for invitation-only users, featuring workout logging, progress tracking with E1RM calculations, historical reporting, and a plate calculator utility.

## ✅ Completed

- [x] Solution structure with clean architecture (Core/Data/Web layers)
- [x] Entity Framework Core + PostgreSQL integration
- [x] ASP.NET Core Identity implementation with User entity
- [x] Core domain entities (User, TrainingCycle, WorkoutWeek, WorkoutDay, ProgrammedSet, CompletedSet, etc.)
- [x] Repository pattern with generic base repository
- [x] Database migrations infrastructure
- [x] Bootstrap 5 integration
- [x] Basic MVC controllers and views scaffolding

## Phase 1: Core Authentication & Authorization

### 1.1 Invitation-Only Registration System
- [ ] Create `UserInvitation` entity with invitation code, email, expiration date, and used status
- [ ] Create `IUserInvitationRepository` interface and implementation
- [ ] Build admin controller/page to generate invitation codes (restrict to admin role)
- [ ] Modify registration flow to require valid invitation code
- [ ] Add invitation code validation during registration
- [ ] Mark invitation as used after successful registration
- [ ] Add email notification when invitation is generated (optional - can defer)

### 1.2 Role-Based Authorization
- [ ] Seed admin role and initial admin user in database
- [ ] Add roles to ASP.NET Core Identity (Admin, User)
- [ ] Implement authorization policies for admin-only features
- [ ] Add `[Authorize]` attributes to controllers requiring authentication
- [ ] Create admin dashboard view for managing users and invitations

### 1.3 User Profile Management
- [ ] Create user profile page showing FirstName, LastName, Email
- [ ] Add ability to update profile information
- [ ] Add ability to change password
- [ ] Add user settings/preferences (units: lbs/kg, default rounding factor, etc.)

## Phase 2: Exercise Management

### 2.1 Exercise Type Management
- [ ] Create `ExerciseController` with CRUD operations
- [ ] Build views for listing, creating, editing, and deleting exercise types
- [ ] Seed common exercises (Squat, Bench Press, Deadlift, Overhead Press, Barbell Row)
- [ ] Add exercise search/filter functionality
- [ ] Ensure exercises are user-scoped (each user has their own exercises)

### 2.2 One-Rep Max (1RM) Management
- [ ] Expand `UserMax` entity if needed (add date tracking, notes)
- [ ] Create `UserMaxController` for managing 1RMs
- [ ] Build views to view/edit current 1RMs for each exercise
- [ ] Add automatic 1RM calculation based on completed sets (using Epley or Brzycki formula)
- [ ] Track 1RM history over time
- [ ] Add E1RM (Estimated 1RM) calculation service in SetStats.Core

## Phase 3: Workout Management (StrongLifts Core)

### 3.1 Training Program Setup
- [ ] Create `TrainingProgramController` for program CRUD
- [ ] Build views to create custom training programs
- [ ] Seed StrongLifts 5x5 program template
- [ ] Allow users to create programs with configurable exercises per workout day
- [ ] Link programs to training cycles

### 3.2 Active Workout Session
- [ ] Create `WorkoutSessionController` for managing current workouts
- [ ] Build "Start Workout" view showing today's programmed sets
- [ ] Implement live workout tracking interface:
  - Display current exercise, target weight, target reps
  - Input actual weight, actual reps, and RPE per set
  - Add set completion buttons (Complete/Skip)
  - Show rest timer between sets
  - Add notes per set or workout
- [ ] Calculate and display suggested weight based on training max and percentage
- [ ] Auto-progress to next set after completion
- [ ] Save completed sets to database
- [ ] Mark WorkoutDay status as Completed when all sets done

### 3.3 Workout History
- [ ] Create `WorkoutHistoryController` for viewing past workouts
- [ ] Build calendar view of completed workouts
- [ ] Display workout detail view showing all sets, weights, reps, RPE
- [ ] Add ability to edit/delete historical workout data
- [ ] Filter workouts by date range, exercise type

### 3.4 Workout Scheduling
- [ ] Build weekly workout calendar view
- [ ] Auto-generate WorkoutWeeks and WorkoutDays based on training program
- [ ] Allow manual schedule adjustments
- [ ] Handle deload weeks (WeekType.Deload)

## Phase 4: Plate Calculator Utility

### 4.1 Plate Calculator Service
- [ ] Create `PlateCalculatorService` in SetStats.Core
- [ ] Implement algorithm to calculate optimal plate combination for target weight
- [ ] Support standard plate sets (45, 35, 25, 10, 5, 2.5 lbs) and metric equivalents
- [ ] Account for barbell weight (45 lbs / 20 kg standard)
- [ ] Return plates needed per side of barbell

### 4.2 Plate Calculator UI
- [ ] Create `PlateCalculatorController` and views
- [ ] Build simple input form (target weight, available plates, bar weight)
- [ ] Display visual representation of plate loading
- [ ] Integrate plate calculator into active workout session view
- [ ] Add user settings to configure available plates

## Phase 5: Progress Tracking & Reporting

### 5.1 Progress Dashboard
- [ ] Create `DashboardController` with overview statistics
- [ ] Display current training cycle progress
- [ ] Show recent workout summary
- [ ] Display current 1RMs for main lifts
- [ ] Show total weight lifted over time period
- [ ] Add quick access to start today's workout

### 5.2 Exercise Progress Reports
- [ ] Create `ReportsController` for data visualization
- [ ] Build chart showing weight progression over time per exercise (using Chart.js or similar)
- [ ] Graph E1RM progression over time
- [ ] Display volume (sets × reps × weight) trends
- [ ] Show average RPE over time
- [ ] Add date range filters for reports
- [ ] Export report data to CSV

### 5.3 Personal Records Tracking
- [ ] Track and display personal records (heaviest weight, most reps, highest volume)
- [ ] Show PR achievements on dashboard
- [ ] Add notification/badge when new PR is set
- [ ] Historical PR tracking

### 5.4 Analytics & Insights
- [ ] Calculate training consistency (workouts completed vs planned)
- [ ] Show workout frequency statistics
- [ ] Identify exercises needing deload
- [ ] Track injury/rest days

## Phase 6: UI/UX Polish

### 6.1 Bootstrap 5 Theming
- [ ] Create custom Bootstrap theme matching personal preferences
- [ ] Ensure mobile-responsive design for all pages
- [ ] Add dark mode support (optional)
- [ ] Implement consistent spacing and typography
- [ ] Add loading spinners for async operations

### 6.2 User Experience Enhancements
- [ ] Add toast notifications for success/error messages
- [ ] Implement client-side form validation
- [ ] Add confirmation dialogs for destructive actions
- [ ] Improve navigation structure (breadcrumbs, active nav items)
- [ ] Add keyboard shortcuts for workout logging (power user feature)
- [ ] Implement progressive web app (PWA) features for mobile use

### 6.3 Performance Optimization
- [ ] Add database indexes for common queries
- [ ] Implement caching for frequently accessed data
- [ ] Optimize EF Core queries (avoid N+1 problems)
- [ ] Add pagination for large data lists
- [ ] Minimize JavaScript bundle size

## Phase 7: Fly.io Deployment

### 7.1 Production Configuration
- [ ] Create production `appsettings.Production.json`
- [ ] Configure connection strings for production PostgreSQL
- [ ] Set up environment variables for secrets
- [ ] Configure logging (consider Serilog with structured logging)
- [ ] Add health check endpoints
- [ ] Configure HTTPS redirection and HSTS

### 7.2 Fly.io Setup
- [ ] Install Fly CLI (`flyctl`)
- [ ] Create Fly.io account
- [ ] Provision Fly Postgres database (`fly postgres create`)
- [ ] Create Dockerfile for .NET 9 application
- [ ] Create `fly.toml` configuration file
- [ ] Configure volume mounts if needed
- [ ] Set environment variables via `fly secrets set`

### 7.3 Database Migration Strategy
- [ ] Apply migrations on app startup or via separate migration job
- [ ] Seed initial data (admin user, default exercises)
- [ ] Set up database backup strategy (Fly Postgres auto-backups)
- [ ] Document database connection and migration procedures

### 7.4 Deployment & Monitoring
- [ ] Deploy application (`fly deploy`)
- [ ] Configure custom domain (optional)
- [ ] Set up SSL certificates
- [ ] Configure Fly.io health checks
- [ ] Set up monitoring and alerts (Fly metrics, Application Insights, or similar)
- [ ] Configure auto-scaling if needed (probably not for personal use)
- [ ] Test full deployment and functionality

### 7.5 Maintenance & Operations
- [ ] Document deployment process
- [ ] Set up CI/CD pipeline (GitHub Actions) for automated deployments
- [ ] Create database backup/restore procedures
- [ ] Plan for zero-downtime deployments
- [ ] Monitor costs (Fly.io pricing)

## Phase 8: Testing (Optional but Recommended)

### 8.1 Unit Testing
- [ ] Create `SetStats.Core.Tests` project
- [ ] Add xUnit or NUnit
- [ ] Write unit tests for PlateCalculatorService
- [ ] Write unit tests for E1RM calculation logic
- [ ] Write unit tests for business logic in Core layer

### 8.2 Integration Testing
- [ ] Create `SetStats.Data.Tests` project
- [ ] Test repository implementations with in-memory database
- [ ] Test data access patterns

### 8.3 End-to-End Testing
- [ ] Create `SetStats.Web.Tests` project
- [ ] Add integration tests for controllers and views
- [ ] Test authentication flows
- [ ] Test key user workflows (start workout, log sets, view progress)

## Phase 9: Nice-to-Have Features (Future Enhancements)

- [ ] Mobile app (Xamarin/MAUI or React Native)
- [ ] Rest timer with notifications
- [ ] Workout templates and program library
- [ ] Exercise instruction videos/images
- [ ] Social features (share PRs, compare with friends)
- [ ] Advanced analytics (periodization tracking, fatigue management)
- [ ] Integration with fitness trackers (optional)
- [ ] Barbell path tracking via phone camera/ML (very advanced)
- [ ] Multi-language support
- [ ] Export data to other platforms

---

## Recommended Development Approach

### Technology Stack Confirmation
✅ ASP.NET Core 9.0 MVC
✅ Entity Framework Core 9.0
✅ PostgreSQL (Npgsql provider)
✅ Bootstrap 5
✅ ASP.NET Core Identity
✅ Clean Architecture (Core/Data/Web)
✅ Repository Pattern

### Hosting Recommendation: Fly.io
Fly.io is an excellent choice for personal projects because:
- Free tier or low monthly cost (~$5-10/month for small app + Postgres)
- Simple deployment with `flyctl`
- Built-in PostgreSQL support with automatic backups
- Global edge network (low latency)
- Easy scaling if needed
- No credit card required for trial

**Alternative Hosting Options:**
- **Azure App Service**: Good .NET integration, free tier available, but can get expensive
- **Railway**: Similar to Fly.io, very developer-friendly
- **Render**: Free tier for web apps, managed PostgreSQL
- **DigitalOcean App Platform**: Simple, predictable pricing
- **Self-hosted VPS**: Cheapest long-term but requires more maintenance

### Development Workflow
1. **Start with Phase 1-3** to get core functionality working locally
2. **Implement Phase 4-5** for the key features (plate calculator, reporting)
3. **Polish UI in Phase 6** once core features are solid
4. **Deploy to Fly.io (Phase 7)** after local testing is complete
5. **Add testing (Phase 8)** as you go or after v1 is deployed
6. **Iterate on Phase 9** features based on personal usage and needs

### Estimated Timeline (Part-Time Development)
- **Phases 1-3**: 3-4 weeks (core authentication and workout tracking)
- **Phases 4-5**: 2-3 weeks (plate calculator and reporting)
- **Phase 6**: 1-2 weeks (UI polish)
- **Phase 7**: 1 week (deployment and configuration)
- **Total**: ~2-3 months for full v1.0 release

This is an aggressive timeline; adjust based on available time and complexity encountered during development.

---

## Next Immediate Steps

1. Review and prioritize this roadmap based on your needs
2. Start with Phase 1.1 (Invitation System) to lock down access
3. Build out core workout tracking (Phase 3) since the domain model is already in place
4. Implement plate calculator (Phase 4) as a standalone utility first
5. Add reporting and charts (Phase 5) once you have real workout data
6. Deploy to Fly.io (Phase 7) when ready for production use

Good luck building your personal StrongLifts app! 💪
