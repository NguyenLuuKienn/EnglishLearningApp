# English Learning App - Implementation Plan

## Overview

Build a complete English Learning System using Clean Architecture + CQRS pattern in .NET 8 (Backend) và ReactJS + TailwindCSS (Frontend).

---

## Current State Analysis

| Layer | Status |
|-------|--------|
| **Domain** | ✅ Entities, Enums, Interfaces, ErrorMessages, BaseEntity hoàn tất |
| **Application** | ✅ CQRS (Vocabulary, Quiz, QuizResult), DTOs, AutoMapper, Result, PagedResult hoàn tất |
| **Infrastructure** | ✅ DbContext, Configurations, Repositories, UnitOfWork hoàn tất |
| **WebAPI** | ✅ Controllers, Middleware, Program.cs, appsettings, Request contracts hoàn tất |
| **Frontend** | ❌ Chưa có — cần tạo ReactJS + TailwindCSS project |
| **Cross-cutting** | ⚠️ Migration đang fix (cascade path), Auth/Leaderboard/History chưa có |

---

## Architecture

```
Backend: Clean Architecture + CQRS
├── Repository Pattern
├── Unit of Work
├── Mediator Pattern (MediatR)
├── SOLID Principles
└── Separation of Concerns

Frontend: ReactJS + TailwindCSS
├── Vite (Build Tool)
├── React Router (Routing)
├── React Query (Data Fetching)
├── Axios (HTTP Client)
└── TailwindCSS (Styling)
```

---

## Phase 1: Domain Layer Foundation

| # | Task | File | Status |
|---|------|------|--------|
| 1.0 | BaseEntity | [.task/task_01_00_base_entity.md](../.task/task_01_00_base_entity.md) | ✅ Done |
| 1.1 | Implement Enums | [.task/task_01_01_implement_enums.md](../.task/task_01_01_implement_enums.md) | ✅ Done |
| 1.2 | Entity: Vocabulary | [.task/task_01_02_entity_vocabulary.md](../.task/task_01_02_entity_vocabulary.md) | ✅ Done |
| 1.3 | Entity: Quiz | [.task/task_01_03_entity_quiz.md](../.task/task_01_03_entity_quiz.md) | ✅ Done |
| 1.4 | Entity: Question | [.task/task_01_04_entity_question.md](../.task/task_01_04_entity_question.md) | ✅ Done |
| 1.5 | Entity: Choice | [.task/task_01_05_entity_choice.md](../.task/task_01_05_entity_choice.md) | ✅ Done |
| 1.6 | Entity: QuizResult | [.task/task_01_06_entity_quiz_result.md](../.task/task_01_06_entity_quiz_result.md) | ✅ Done |
| 1.7 | ErrorMessages | [.task/task_01_07_error_messages.md](../.task/task_01_07_error_messages.md) | ✅ Done |
| 1.8 | Domain Interfaces | [.task/task_01_08_domain_interfaces.md](../.task/task_01_08_domain_interfaces.md) | ✅ Done |

---

## Phase 2: Application Layer

| # | Task | File | Status |
|---|------|------|--------|
| 2.1 | Setup Dependencies | [.task/task_02_01_setup_application_dependencies.md](../.task/task_02_01_setup_application_dependencies.md) | ✅ Done |
| 2.2 | Common Classes (Result, PagedResult) | [.task/task_02_02_common_classes.md](../.task/task_02_02_common_classes.md) | ✅ Done |
| 2.3 | Create DTOs | [.task/task_02_03_create_dtos.md](../.task/task_02_03_create_dtos.md) | ✅ Done |
| 2.4 | CQRS: Vocabulary Features | [.task/task_02_04_cqrs_vocabulary.md](../.task/task_02_04_cqrs_vocabulary.md) | ✅ Done |
| 2.5 | CQRS: Quiz Features | [.task/task_02_05_cqrs_quiz.md](../.task/task_02_05_cqrs_quiz.md) | ✅ Done |
| 2.6 | CQRS: QuizResult Features | [.task/task_02_06_cqrs_quiz_result.md](../.task/task_02_06_cqrs_quiz_result.md) | ✅ Done |
| 2.7 | AutoMapper Profiles | [.task/task_02_07_automapper_profiles.md](../.task/task_02_07_automapper_profiles.md) | ✅ Done |
| 2.8 | DependencyInjection | [.task/task_02_08_application_di.md](../.task/task_02_08_application_di.md) | ✅ Done |

---

## Phase 3: Infrastructure Layer

| # | Task | File | Status |
|---|------|------|--------|
| 3.1 | Setup Dependencies | [.task/task_03_01_setup_infrastructure_dependencies.md](../.task/task_03_01_setup_infrastructure_dependencies.md) | ✅ Done |
| 3.2 | Create DbContext | [.task/task_03_02_create_dbcontext.md](../.task/task_03_02_create_dbcontext.md) | ✅ Done |
| 3.3 | Entity Configurations | [.task/task_03_03_entity_configurations.md](../.task/task_03_03_entity_configurations.md) | ✅ Done |
| 3.4 | Base Repository | [.task/task_03_04_base_repository.md](../.task/task_03_04_base_repository.md) | ✅ Done |
| 3.5 | Specific Repositories | [.task/task_03_05_specific_repositories.md](../.task/task_03_05_specific_repositories.md) | ✅ Done |
| 3.6 | Unit of Work | [.task/task_03_06_unit_of_work.md](../.task/task_03_06_unit_of_work.md) | ✅ Done |
| 3.7 | DependencyInjection | [.task/task_03_07_infrastructure_di.md](../.task/task_03_07_infrastructure_di.md) | ✅ Done |

---

## Phase 4: WebAPI Layer

| # | Task | File | Status |
|---|------|------|--------|
| 4.1 | Setup Dependencies | [.task/task_04_01_setup_webapi_dependencies.md](../.task/task_04_01_setup_webapi_dependencies.md) | ✅ Done |
| 4.2 | API Response Models | [.task/task_04_02_api_response_models.md](../.task/task_04_02_api_response_models.md) | ✅ Done |
| 4.3 | Request Contracts | [.task/task_04_03_request_contracts.md](../.task/task_04_03_request_contracts.md) | ✅ Done |
| 4.4 | Create Controllers | [.task/task_04_04_create_controllers.md](../.task/task_04_04_create_controllers.md) | ✅ Done |
| 4.5 | Exception Middleware | [.task/task_04_05_exception_middleware.md](../.task/task_04_05_exception_middleware.md) | ✅ Done |
| 4.6 | Update Program.cs | [.task/task_04_06_update_program.cs.md](../.task/task_04_06_update_program.cs.md) | ✅ Done |
| 4.7 | Update appsettings.json | [.task/task_04_07_update_appsettings.md](../.task/task_04_07_update_appsettings.md) | ✅ Done |
| 4.8 | Cleanup Template Files | [.task/task_04_08_cleanup_template_files.md](../.task/task_04_08_cleanup_template_files.md) | ✅ Done |

---

## Phase 5: Database Migration & Seeding

| # | Task | File | Status |
|---|------|------|--------|
| 5.1 | Initial Migration | [.task/task_05_01_initial_migration.md](../.task/task_05_01_initial_migration.md) | 🔄 In Progress |
| 5.2 | Data Seeding | [.task/task_05_02_data_seeding.md](../.task/task_05_02_data_seeding.md) | ⬜ Pending |

---

## Phase 6: Authentication & Authorization

> **Mục tiêu:** Xây dựng hệ thống đăng ký/đăng nhập với JWT + Refresh Token, user tự chọn role (Student/Teacher).

### 6.1 Domain — User Entity

| # | Task | File | Status |
|---|------|------|--------|
| 6.1 | Entity: User (Username, Email, PasswordHash, Role, Avatar) | [.task/task_06_01_entity_user.md](../.task/task_06_01_entity_user.md) | ✅ Done |
| 6.2 | Enum: UserRole (Admin, Teacher, Student) | [.task/task_06_02_enum_user_role.md](../.task/task_06_02_enum_user_role.md) | ✅ Done |
| 6.3 | Interface: IUserRepository | [.task/task_06_03_iuser_repository.md](../.task/task_06_03_iuser_repository.md) | ✅ Done |

### 6.2 Application — Auth CQRS

| # | Task | File | Status |
|---|------|------|--------|
| 6.4 | DTOs: UserDto, TokenDto | [.task/task_06_04_auth_dtos.md](../.task/task_06_04_auth_dtos.md) | ⬜ Pending |
| 6.5 | ITokenService Interface | [.task/task_06_05_token_service_interface.md](../.task/task_06_05_token_service_interface.md) | ⬜ Pending |
| 6.6 | Command: Register (user chọn role Student/Teacher) | [.task/task_06_06_register_command.md](../.task/task_06_06_register_command.md) | ⬜ Pending |
| 6.7 | Command: Login | [.task/task_06_07_login_command.md](../.task/task_06_07_login_command.md) | ⬜ Pending |
| 6.8 | Command: RefreshToken | [.task/task_06_08_refreshtoken_command.md](../.task/task_06_08_refreshtoken_command.md) | ⬜ Pending |
| 6.9 | Query: GetProfile | [.task/task_06_09_getprofile_query.md](../.task/task_06_09_getprofile_query.md) | ⬜ Pending |

### 6.3 Infrastructure — Auth Implementation

| # | Task | File | Status |
|---|------|------|--------|
| 6.10 | UserRepository + Configuration | [.task/task_06_10_user_repository.md](../.task/task_06_10_user_repository.md) | ⬜ Pending |
| 6.11 | TokenService Implementation | [.task/task_06_11_token_service_impl.md](../.task/task_06_11_token_service_impl.md) | ⬜ Pending |
| 6.12 | Auth Migration + Seed Admin | [.task/task_06_12_auth_migration.md](../.task/task_06_12_auth_migration.md) | ⬜ Pending |

### 6.4 WebAPI — Auth Controller

| # | Task | File | Status |
|---|------|------|--------|
| 6.13 | Auth Request Contracts | [.task/task_06_13_auth_requests.md](../.task/task_06_13_auth_requests.md) | ⬜ Pending |
| 6.14 | AuthController | [.task/task_06_14_auth_controller.md](../.task/task_06_14_auth_controller.md) | ⬜ Pending |
| 6.15 | Configure JWT + DI + [Authorize] | [.task/task_06_15_configure_jwt_di.md](../.task/task_06_15_configure_jwt_di.md) | ⬜ Pending |

---

## Phase 7: Quiz Assignment & Scheduling

> **Mục tiêu:** Admin/Teacher gán bài test cho role/user cụ thể, bài test có thời gian bắt đầu & kết thúc.

### 7.1 Domain — Quiz Assignment

| # | Task | File | Status |
|---|------|------|--------|
| 7.1 | Entity: QuizAssignment (QuizId, TargetRole, TargetUserId, StartTime, EndTime, Status) | [.task/task_07_01_entity_quiz_assignment.md](../.task/task_07_01_entity_quiz_assignment.md) | ⬜ Pending |
| 7.2 | Enum: AssignmentStatus (Scheduled, Active, Completed, Cancelled) | [.task/task_07_02_enum_assignment_status.md](../.task/task_07_02_enum_assignment_status.md) | ⬜ Pending |
| 7.3 | Update Quiz Entity (StartTime, EndTime nullable) | [.task/task_07_03_update_quiz_entity.md](../.task/task_07_03_update_quiz_entity.md) | ⬜ Pending |
| 7.4 | Interface: IQuizAssignmentRepository | [.task/task_07_04_iquiz_assignment_repository.md](../.task/task_07_04_iquiz_assignment_repository.md) | ⬜ Pending |

### 7.2 Application — Assignment CQRS

| # | Task | File | Status |
|---|------|------|--------|
| 7.5 | DTOs: QuizAssignmentDto | [.task/task_07_05_assignment_dtos.md](../.task/task_07_05_assignment_dtos.md) | ⬜ Pending |
| 7.6 | Command: AssignQuizCommand (gán quiz cho role hoặc user) | [.task/task_07_06_assign_quiz_command.md](../.task/task_07_06_assign_quiz_command.md) | ⬜ Pending |
| 7.7 | Command: CancelAssignmentCommand | [.task/task_07_07_cancel_assignment_command.md](../.task/task_07_07_cancel_assignment_command.md) | ⬜ Pending |
| 7.8 | Query: GetUserAssignmentsQuery (lấy danh sách assignment của user) | [.task/task_07_08_get_user_assignments_query.md](../.task/task_07_08_get_user_assignments_query.md) | ⬜ Pending |
| 7.9 | Query: GetActiveAssignmentsQuery (lấy assignment đang active) | [.task/task_07_09_get_active_assignments_query.md](../.task/task_07_09_get_active_assignments_query.md) | ⬜ Pending |
| 7.10 | Query: GetAssignmentByIdQuery | [.task/task_07_10_get_assignment_by_id_query.md](../.task/task_07_10_get_assignment_by_id_query.md) | ⬜ Pending |

### 7.3 Infrastructure — Assignment Implementation

| # | Task | File | Status |
|---|------|------|--------|
| 7.11 | QuizAssignmentRepository + Configuration | [.task/task_07_11_assignment_repository.md](../.task/task_07_11_assignment_repository.md) | ⬜ Pending |
| 7.12 | Migration: Add QuizAssignments table + update Quizzes | [.task/task_07_12_assignment_migration.md](../.task/task_07_12_assignment_migration.md) | ⬜ Pending |

### 7.4 WebAPI — Assignment Controller

| # | Task | File | Status |
|---|------|------|--------|
| 7.13 | AssignmentController (Assign, Cancel, GetUserAssignments, GetActive) | [.task/task_07_13_assignment_controller.md](../.task/task_07_13_assignment_controller.md) | ⬜ Pending |
| 7.14 | Request Contracts: AssignQuizRequest | [.task/task_07_14_assignment_requests.md](../.task/task_07_14_assignment_requests.md) | ⬜ Pending |

---

## Phase 8: Notification & Background Jobs

> **Mục tiêu:** Hệ thống thông báo + background job tự động gửi notification khi assign quiz, quiz bắt đầu, kết thúc.

### 8.1 Domain — Notification

| # | Task | File | Status |
|---|------|------|--------|
| 8.1 | Entity: Notification (UserId, Type, Title, Message, IsRead, Data) | [.task/task_08_01_entity_notification.md](../.task/task_08_01_entity_notification.md) | ⬜ Pending |
| 8.2 | Enum: NotificationType (QuizAssigned, QuizStartingSoon, QuizStarted, QuizEnded, QuizResultAvailable) | [.task/task_08_02_enum_notification_type.md](../.task/task_08_02_enum_notification_type.md) | ⬜ Pending |
| 8.3 | Interface: INotificationRepository | [.task/task_08_03_inotification_repository.md](../.task/task_08_03_inotification_repository.md) | ⬜ Pending |

### 8.2 Application — Notification Service

| # | Task | File | Status |
|---|------|------|--------|
| 8.4 | INotificationService Interface (Send, SendToRole, SendToUser) | [.task/task_08_04_notification_service_interface.md](../.task/task_08_04_notification_service_interface.md) | ⬜ Pending |
| 8.5 | DTOs: NotificationDto | [.task/task_08_05_notification_dtos.md](../.task/task_08_05_notification_dtos.md) | ⬜ Pending |
| 8.6 | Query: GetUserNotificationsQuery (paged, filter by read status) | [.task/task_08_06_get_user_notifications_query.md](../.task/task_08_06_get_user_notifications_query.md) | ⬜ Pending |
| 8.7 | Command: MarkNotificationReadCommand | [.task/task_08_07_mark_notification_read_command.md](../.task/task_08_07_mark_notification_read_command.md) | ⬜ Pending |

### 8.3 Infrastructure — Notification & Jobs

| # | Task | File | Status |
|---|------|------|--------|
| 8.8 | NotificationRepository + Configuration | [.task/task_08_08_notification_repository.md](../.task/task_08_08_notification_repository.md) | ⬜ Pending |
| 8.9 | NotificationService Implementation | [.task/task_08_09_notification_service_impl.md](../.task/task_08_09_notification_service_impl.md) | ⬜ Pending |
| 8.10 | Setup Hangfire (background job scheduler) | [.task/task_08_10_setup_hangfire.md](../.task/task_08_10_setup_hangfire.md) | ⬜ Pending |
| 8.11 | Background Job: CheckQuizAssignments (kiểm tra quiz sắp bắt đầu/đã kết thúc) | [.task/task_08_11_job_check_assignments.md](../.task/task_08_11_job_check_assignments.md) | ⬜ Pending |
| 8.12 | Background Job: SendAssignmentNotifications (gửi notification khi assign mới) | [.task/task_08_12_job_send_notifications.md](../.task/task_08_12_job_send_notifications.md) | ⬜ Pending |
| 8.13 | Migration: Add Notifications table | [.task/task_08_13_notification_migration.md](../.task/task_08_13_notification_migration.md) | ⬜ Pending |

### 8.4 WebAPI — Notification Controller

| # | Task | File | Status |
|---|------|------|--------|
| 8.14 | NotificationController (GetUserNotifications, MarkRead) | [.task/task_08_14_notification_controller.md](../.task/task_08_14_notification_controller.md) | ⬜ Pending |

---

## Phase 9: History & Leaderboard

> **Mục tiêu:** Theo dõi lịch sử học tập, bảng xếp hạng người dùng.

### 9.1 Domain — History & Leaderboard

| # | Task | File | Status |
|---|------|------|--------|
| 9.1 | Enum: ActionType (ViewVocabulary, CompleteQuiz, BookmarkWord, StartQuiz) | [.task/task_09_01_enum_action_type.md](../.task/task_09_01_enum_action_type.md) | ⬜ Pending |
| 9.2 | Entity: LearningHistory | [.task/task_09_02_entity_learning_history.md](../.task/task_09_02_entity_learning_history.md) | ⬜ Pending |
| 9.3 | Entity: Leaderboard | [.task/task_09_03_entity_leaderboard.md](../.task/task_09_03_entity_leaderboard.md) | ⬜ Pending |
| 9.4 | Interfaces: ILearningHistoryRepository, ILeaderboardRepository | [.task/task_09_04_history_leaderboard_interfaces.md](../.task/task_09_04_history_leaderboard_interfaces.md) | ⬜ Pending |

### 9.2 Application — History & Leaderboard CQRS

| # | Task | File | Status |
|---|------|------|--------|
| 9.5 | DTOs: LearningHistoryDto, LeaderboardDto | [.task/task_09_05_history_leaderboard_dtos.md](../.task/task_09_05_history_leaderboard_dtos.md) | ⬜ Pending |
| 9.6 | Command: RecordHistoryCommand | [.task/task_09_06_record_history_command.md](../.task/task_09_06_record_history_command.md) | ⬜ Pending |
| 9.7 | Query: GetUserHistoryQuery | [.task/task_09_07_get_user_history_query.md](../.task/task_09_07_get_user_history_query.md) | ⬜ Pending |
| 9.8 | Command: UpdateLeaderboardCommand | [.task/task_09_08_update_leaderboard_command.md](../.task/task_09_08_update_leaderboard_command.md) | ⬜ Pending |
| 9.9 | Query: GetLeaderboardQuery & GetUserRankQuery | [.task/task_09_09_leaderboard_queries.md](../.task/task_09_09_leaderboard_queries.md) | ⬜ Pending |

### 9.3 Infrastructure — History & Leaderboard Implementation

| # | Task | File | Status |
|---|------|------|--------|
| 9.10 | Repositories + Configurations | [.task/task_09_10_history_leaderboard_repositories.md](../.task/task_09_10_history_leaderboard_repositories.md) | ⬜ Pending |
| 9.11 | Migration: Add LearningHistories + Leaderboards | [.task/task_09_11_history_migration.md](../.task/task_09_11_history_migration.md) | ⬜ Pending |

### 9.4 WebAPI — History & Leaderboard Controllers

| # | Task | File | Status |
|---|------|------|--------|
| 9.12 | HistoryController + LeaderboardController | [.task/task_09_12_history_leaderboard_controllers.md](../.task/task_09_12_history_leaderboard_controllers.md) | ⬜ Pending |
| 9.13 | Integrate History into QuizResult handler | [.task/task_09_13_integrate_history.md](../.task/task_09_13_integrate_history.md) | ⬜ Pending |

---

## Phase 10: Frontend — ReactJS + TailwindCSS

> **Mục tiêu:** Xây dựng giao diện người dùng hoàn chỉnh với ReactJS + TailwindCSS + Vite.

### 10.1 Project Setup

| # | Task | File | Status |
|---|------|------|--------|
| 10.1 | Create React + TypeScript project with Vite | [.task/task_10_01_setup_react.md](../.task/task_10_01_setup_react.md) | ⬜ Pending |
| 10.2 | Configure TailwindCSS + PostCSS | [.task/task_10_02_setup_tailwind.md](../.task/task_10_02_setup_tailwind.md) | ⬜ Pending |
| 10.3 | Install dependencies (React Router, React Query, Axios, Lucide Icons) | [.task/task_10_03_install_dependencies.md](../.task/task_10_03_install_dependencies.md) | ⬜ Pending |
| 10.4 | Setup folder structure (pages, components, services, hooks, types, store) | [.task/task_10_04_folder_structure.md](../.task/task_10_04_folder_structure.md) | ⬜ Pending |

### 10.2 Core Infrastructure

| # | Task | File | Status |
|---|------|------|--------|
| 10.5 | TypeScript Types (User, Quiz, Assignment, Notification, History, Leaderboard) | [.task/task_10_05_types.md](../.task/task_10_05_types.md) | ⬜ Pending |
| 10.6 | API Service Layer (Axios instance, interceptors, auth token) | [.task/task_10_06_api_service.md](../.task/task_10_06_api_service.md) | ⬜ Pending |
| 10.7 | Auth Context + Custom Hooks (useAuth, useApi) | [.task/task_10_07_auth_context.md](../.task/task_10_07_auth_context.md) | ⬜ Pending |
| 10.8 | React Query Setup (QueryClient, providers) | [.task/task_10_08_react_query.md](../.task/task_10_08_react_query.md) | ⬜ Pending |
| 10.9 | Router Configuration (public routes, protected routes, role-based routes) | [.task/task_10_09_router.md](../.task/task_10_09_router.md) | ⬜ Pending |

### 10.3 Shared Components

| # | Task | File | Status |
|---|------|------|--------|
| 10.10 | Layout: Navbar, Sidebar, MainLayout | [.task/task_10_10_layout.md](../.task/task_10_10_layout.md) | ⬜ Pending |
| 10.11 | UI Components: Button, Input, Card, Modal, Badge, Spinner | [.task/task_10_11_ui_components.md](../.task/task_10_11_ui_components.md) | ⬜ Pending |

### 10.4 Auth Pages

| # | Task | File | Status |
|---|------|------|--------|
| 10.12 | Login Page (form, validation, error handling) | [.task/task_10_12_login_page.md](../.task/task_10_12_login_page.md) | ⬜ Pending |
| 10.13 | Register Page (form, role selection: Student/Teacher) | [.task/task_10_13_register_page.md](../.task/task_10_13_register_page.md) | ⬜ Pending |
| 10.14 | Profile Page (user info, avatar, stats, role badge) | [.task/task_10_14_profile_page.md](../.task/task_10_14_profile_page.md) | ⬜ Pending |

### 10.5 Feature Pages

| # | Task | File | Status |
|---|------|------|--------|
| 10.15 | Dashboard Page (stats, assigned quizzes, notifications, quick actions) | [.task/task_10_15_dashboard_page.md](../.task/task_10_15_dashboard_page.md) | ⬜ Pending |
| 10.16 | Vocabulary List Page (search, filter by difficulty, pagination) | [.task/task_10_16_vocabulary_page.md](../.task/task_10_16_vocabulary_page.md) | ⬜ Pending |
| 10.17 | Vocabulary Detail / Flashcard View | [.task/task_10_17_vocabulary_detail.md](../.task/task_10_17_vocabulary_detail.md) | ⬜ Pending |
| 10.18 | Quiz List Page (assigned quizzes, filter by status: scheduled/active/completed) | [.task/task_10_18_quiz_list_page.md](../.task/task_10_18_quiz_list_page.md) | ⬜ Pending |
| 10.19 | Quiz Take Page (timer, countdown, question navigation, submit) | [.task/task_10_19_quiz_take_page.md](../.task/task_10_19_quiz_take_page.md) | ⬜ Pending |
| 10.20 | Quiz Result Page (score, review answers, correct/incorrect) | [.task/task_10_20_quiz_result_page.md](../.task/task_10_20_quiz_result_page.md) | ⬜ Pending |
| 10.21 | History Page (learning activity timeline, filter) | [.task/task_10_21_history_page.md](../.task/task_10_21_history_page.md) | ⬜ Pending |
| 10.22 | Leaderboard Page (ranking table, period filter, user highlight) | [.task/task_10_22_leaderboard_page.md](../.task/task_10_22_leaderboard_page.md) | ⬜ Pending |
| 10.23 | Notifications Page (notification list, mark as read) | [.task/task_10_23_notifications_page.md](../.task/task_10_23_notifications_page.md) | ⬜ Pending |

### 10.6 Admin/Teacher Pages

| # | Task | File | Status |
|---|------|------|--------|
| 10.24 | Admin Dashboard (user management, content management, stats) | [.task/task_10_24_admin_dashboard.md](../.task/task_10_24_admin_dashboard.md) | ⬜ Pending |
| 10.25 | Admin: Create/Edit Quiz Page | [.task/task_10_25_admin_quiz.md](../.task/task_10_25_admin_quiz.md) | ⬜ Pending |
| 10.26 | Admin: Assign Quiz Page (assign to role/user, set start/end time) | [.task/task_10_26_admin_assign_quiz.md](../.task/task_10_26_admin_assign_quiz.md) | ⬜ Pending |
| 10.27 | Admin: Manage Vocabulary Page | [.task/task_10_27_admin_vocabulary.md](../.task/task_10_27_admin_vocabulary.md) | ⬜ Pending |

---

## Phase 11: Integration, CORS & Final Testing

| # | Task | File | Status |
|---|------|------|--------|
| 11.1 | Configure CORS in WebAPI for frontend | [.task/task_11_01_configure_cors.md](../.task/task_11_01_configure_cors.md) | ⬜ Pending |
| 11.2 | Setup environment variables for API URL | [.task/task_11_02_env_variables.md](../.task/task_11_02_env_variables.md) | ⬜ Pending |
| 11.3 | End-to-end testing (Register → Login → Assign Quiz → Take Quiz → View Result → Leaderboard) | [.task/task_11_03_e2e_testing.md](../.task/task_11_03_e2e_testing.md) | ⬜ Pending |
| 11.4 | Responsive design review (mobile, tablet, desktop) | [.task/task_11_04_responsive.md](../.task/task_11_04_responsive.md) | ⬜ Pending |
| 11.5 | Build & deploy preparation | [.task/task_11_05_build_deploy.md](../.task/task_11_05_build_deploy.md) | ⬜ Pending |

---

## Dependencies Between Tasks

```
Phase 1 (Domain) → Phase 2 (Application) → Phase 3 (Infrastructure) → Phase 4 (WebAPI)
                                                                    ↓
Phase 5 (Migration) → Phase 6 (Auth) → Phase 7 (Quiz Assignment) → Phase 8 (Notification + Jobs)
                                                                                          ↓
Phase 9 (History + Leaderboard) ← Phase 6 + 7 (APIs ready)
                                                                    ↓
Phase 10 (Frontend) ← Phase 6-9 (all APIs ready)
                                                                    ↓
Phase 11 (Integration & Testing)
```

**Lưu ý:**
- Phase 1-4: Hoàn tất (Backend core)
- Phase 5: Đang fix migration (cascade path)
- Phase 6: Auth (JWT + Refresh Token, user chọn role Student/Teacher)
- Phase 7: Quiz Assignment (gán bài test theo role/user, có thời gian bắt đầu/kết thúc)
- Phase 8: Notification + Hangfire Jobs (thông báo assign, bắt đầu, kết thúc quiz)
- Phase 9: History + Leaderboard (lịch sử học tập, bảng xếp hạng)
- Phase 10: Frontend (có thể bắt đầu song song sau khi Phase 6 hoàn tất API Auth)
- Phase 11: Integration cuối cùng

---

## Notes

- **Database:** SQL Server (localdb for development)
- **Authentication:** JWT + Refresh Token, BCrypt password hashing
- **Roles:** Admin (quản lý), Teacher (tạo quiz, assign), Student (học tập, làm quiz)
- **Frontend:** ReactJS + TypeScript + Vite + TailwindCSS + React Query + Axios
- **Background Jobs:** Hangfire (kiểm tra quiz assignment, gửi notification)
- **CORS:** WebAPI cần cấu hình CORS cho frontend dev server
- **Auto-grading:** Implemented in QuizResult handler (compare submitted answers with correct answers)
- **Leaderboard:** Tính toán dựa trên tổng điểm, số quiz hoàn thành, điểm trung bình, streak
- **History:** Tự động log khi user submit quiz, xem vocabulary, bookmark word
- **Notification Types:** QuizAssigned, QuizStartingSoon, QuizStarted, QuizEnded, QuizResultAvailable
