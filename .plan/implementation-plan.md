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

> **Mục tiêu:** Xây dựng hệ thống đăng ký/đăng nhập với JWT + Refresh Token, phân quyền Admin/User.

### 6.1 Domain — User Entity

| # | Task | File | Status |
|---|------|------|--------|
| 6.1.1 | Entity: User (Username, Email, PasswordHash, Role, Avatar) | [.task/task_06_01_entity_user.md](../.task/task_06_01_entity_user.md) | ⬜ Pending |
| 6.1.2 | Enum: UserRole (Admin, User) | [.task/task_06_02_enum_user_role.md](../.task/task_06_02_enum_user_role.md) | ⬜ Pending |
| 6.1.3 | Interface: IUserRepository | [.task/task/task_06_03_iuser_repository.md](../.task/task_06_03_iuser_repository.md) | ⬜ Pending |

### 6.2 Application — Auth CQRS

| # | Task | File | Status |
|---|------|------|--------|
| 6.2.1 | DTOs: UserDto, LoginRequestDto, RegisterRequestDto, TokenDto | [.task/task_06_04_auth_dtos.md](../.task/task_06_04_auth_dtos.md) | ⬜ Pending |
| 6.2.2 | Command: RegisterCommand + Handler (BCrypt hash, validate) | [.task/task_06_05_register_command.md](../.task/task_06_05_register_command.md) | ⬜ Pending |
| 6.2.3 | Command: LoginCommand + Handler (validate, generate JWT + RefreshToken) | [.task/task_06_06_login_command.md](../.task/task_06_06_login_command.md) | ⬜ Pending |
| 6.2.4 | Command: RefreshTokenCommand + Handler | [.task/task_06_07_refreshtoken_command.md](../.task/task_06_07_refreshtoken_command.md) | ⬜ Pending |
| 6.2.5 | Query: GetProfileQuery + Handler (get current user profile) | [.task/task_06_08_getprofile_query.md](../.task/task_06_08_getprofile_query.md) | ⬜ Pending |
| 6.2.6 | JWT Token Service (ITokenService — Generate, Validate) | [.task/task_06_09_token_service.md](../.task/task_06_09_token_service.md) | ⬜ Pending |

### 6.3 Infrastructure — Auth Implementation

| # | Task | File | Status |
|---|------|------|--------|
| 6.3.1 | UserRepository + UserConfiguration (EF Core) | [.task/task_06_10_user_repository.md](../.task/task_06_10_user_repository.md) | ⬜ Pending |
| 6.3.2 | TokenService Implementation (JWT + RefreshToken) | [.task/task_06_11_token_service_impl.md](../.task/task_06_11_token_service_impl.md) | ⬜ Pending |
| 6.3.3 | Migration: Add Users table + seed admin user | [.task/task_06_12_auth_migration.md](../.task/task_06_12_auth_migration.md) | ⬜ Pending |

### 6.4 WebAPI — Auth Controller & Middleware

| # | Task | File | Status |
|---|------|------|--------|
| 6.4.1 | AuthController (Register, Login, RefreshToken, GetProfile) | [.task/task_06_13_auth_controller.md](../.task/task_06_13_auth_controller.md) | ⬜ Pending |
| 6.4.2 | Request Contracts: RegisterRequest, LoginRequest, RefreshTokenRequest | [.task/task_06_14_auth_requests.md](../.task/task_06_14_auth_requests.md) | ⬜ Pending |
| 6.4.3 | Configure JWT Auth in Program.cs + [Authorize] on controllers | [.task/task_06_15_configure_jwt.md](../.task/task_06_15_configure_jwt.md) | ⬜ Pending |

---

## Phase 7: Advanced Features (History + Leaderboard)

> **Mục tiêu:** Theo dõi lịch sử học tập, bảng xếp hạng người dùng.

### 7.1 Domain — History & Leaderboard

| # | Task | File | Status |
|---|------|------|--------|
| 7.1.1 | Entity: LearningHistory (UserId, ActionType, TargetId, Details, CreatedAt) | [.task/task_07_01_entity_learning_history.md](../.task/task_07_01_entity_learning_history.md) | ⬜ Pending |
| 7.1.2 | Enum: ActionType (ViewVocabulary, CompleteQuiz, BookmarkWord, StartQuiz) | [.task/task_07_02_enum_action_type.md](../.task/task_07_02_enum_action_type.md) | ⬜ Pending |
| 7.1.3 | Entity: Leaderboard (UserId, TotalScore, QuizzesCompleted, AverageScore, Streak, Rank) | [.task/task_07_03_entity_leaderboard.md](../.task/task_07_03_entity_leaderboard.md) | ⬜ Pending |
| 7.1.4 | Interfaces: ILearningHistoryRepository, ILeaderboardRepository | [.task/task_07_04_history_leaderboard_interfaces.md](../.task/task_07_04_history_leaderboard_interfaces.md) | ⬜ Pending |

### 7.2 Application — History & Leaderboard CQRS

| # | Task | File | Status |
|---|------|------|--------|
| 7.2.1 | DTOs: LearningHistoryDto, LeaderboardDto | [.task/task_07_05_history_leaderboard_dtos.md](../.task/task_07_05_history_leaderboard_dtos.md) | ⬜ Pending |
| 7.2.2 | Command: RecordHistoryCommand + Handler (log user action) | [.task/task_07_06_record_history_command.md](../.task/task_07_06_record_history_command.md) | ⬜ Pending |
| 7.2.3 | Query: GetUserHistoryQuery + Handler (paged, filter by action type) | [.task/task_07_07_get_user_history_query.md](../.task/task_07_07_get_user_history_query.md) | ⬜ Pending |
| 7.2.4 | Command: UpdateLeaderboardCommand + Handler (recalculate after quiz) | [.task/task_07_08_update_leaderboard_command.md](../.task/task_07_08_update_leaderboard_command.md) | ⬜ Pending |
| 7.2.5 | Query: GetLeaderboardQuery + Handler (global, filter by period: weekly/monthly/all-time) | [.task/task_07_09_get_leaderboard_query.md](../.task/task_07_09_get_leaderboard_query.md) | ⬜ Pending |
| 7.2.6 | Query: GetUserRankQuery + Handler | [.task/task_07_10_get_user_rank_query.md](../.task/task_07_10_get_user_rank_query.md) | ⬜ Pending |

### 7.3 Infrastructure — History & Leaderboard Implementation

| # | Task | File | Status |
|---|------|------|--------|
| 7.3.1 | LearningHistoryRepository + Configuration | [.task/task_07_11_history_repository.md](../.task/task_07_11_history_repository.md) | ⬜ Pending |
| 7.3.2 | LeaderboardRepository + Configuration | [.task/task_07_12_leaderboard_repository.md](../.task/task_07_12_leaderboard_repository.md) | ⬜ Pending |
| 7.3.3 | Migration: Add LearningHistories + Leaderboards tables | [.task/task_07_13_history_migration.md](../.task/task_07_13_history_migration.md) | ⬜ Pending |

### 7.4 WebAPI — History & Leaderboard Controllers

| # | Task | File | Status |
|---|------|------|--------|
| 7.4.1 | HistoryController (GET /user/history, POST /record) | [.task/task_07_14_history_controller.md](../.task/task_07_14_history_controller.md) | ⬜ Pending |
| 7.4.2 | LeaderboardController (GET /leaderboard, GET /user/rank) | [.task/task_07_15_leaderboard_controller.md](../.task/task_07_15_leaderboard_controller.md) | ⬜ Pending |
| 7.4.3 | Integrate RecordHistory into QuizResult handler (auto-log on quiz submit) | [.task/task_07_16_integrate_history.md](../.task/task_07_16_integrate_history.md) | ⬜ Pending |

---

## Phase 8: Frontend — ReactJS + TailwindCSS

> **Mục tiêu:** Xây dựng giao diện người dùng hoàn chỉnh với ReactJS + TailwindCSS + Vite.

### 8.1 Project Setup

| # | Task | File | Status |
|---|------|------|--------|
| 8.1.1 | Create React + TypeScript project with Vite | [.task/task_08_01_setup_react.md](../.task/task_08_01_setup_react.md) | ⬜ Pending |
| 8.1.2 | Configure TailwindCSS + PostCSS | [.task/task_08_02_setup_tailwind.md](../.task/task_08_02_setup_tailwind.md) | ⬜ Pending |
| 8.1.3 | Install dependencies (React Router, React Query, Axios, Lucide Icons) | [.task/task_08_03_install_dependencies.md](../.task/task_08_03_install_dependencies.md) | ⬜ Pending |
| 8.1.4 | Setup folder structure (pages, components, services, hooks, types, store) | [.task/task_08_04_folder_structure.md](../.task/task_08_04_folder_structure.md) | ⬜ Pending |

### 8.2 Core Infrastructure

| # | Task | File | Status |
|---|------|------|--------|
| 8.2.1 | API Service Layer (Axios instance, interceptors, auth token) | [.task/task_08_05_api_service.md](../.task/task_08_05_api_service.md) | ⬜ Pending |
| 8.2.2 | Auth Context + Custom Hooks (useAuth, useApi) | [.task/task_08_06_auth_context.md](../.task/task_08_06_auth_context.md) | ⬜ Pending |
| 8.2.3 | React Query Setup (QueryClient, providers) | [.task/task_08_07_react_query.md](../.task/task_08_07_react_query.md) | ⬜ Pending |
| 8.2.4 | Router Configuration (public routes, protected routes) | [.task/task_08_08_router.md](../.task/task_08_08_router.md) | ⬜ Pending |
| 8.2.5 | TypeScript Types (User, Vocabulary, Quiz, QuizResult, History, Leaderboard) | [.task/task_08_09_types.md](../.task/task_08_09_types.md) | ⬜ Pending |

### 8.3 Shared Components

| # | Task | File | Status |
|---|------|------|--------|
| 8.3.1 | Layout: Navbar, Sidebar, MainLayout | [.task/task_08_10_layout.md](../.task/task_08_10_layout.md) | ⬜ Pending |
| 8.3.2 | UI Components: Button, Input, Card, Modal, Badge, Spinner | [.task/task_08_11_ui_components.md](../.task/task_08_11_ui_components.md) | ⬜ Pending |
| 8.3.3 | Table Component (sortable, paginated) | [.task/task_08_12_table_component.md](../.task/task_08_12_table_component.md) | ⬜ Pending |

### 8.4 Auth Pages

| # | Task | File | Status |
|---|------|------|--------|
| 8.4.1 | Login Page (form, validation, error handling) | [.task/task_08_13_login_page.md](../.task/task_08_13_login_page.md) | ⬜ Pending |
| 8.4.2 | Register Page (form, validation, success redirect) | [.task/task_08_14_register_page.md](../.task/task_08_14_register_page.md) | ⬜ Pending |
| 8.4.3 | Profile Page (user info, avatar, stats) | [.task/task_08_15_profile_page.md](../.task/task_08_15_profile_page.md) | ⬜ Pending |

### 8.5 Feature Pages

| # | Task | File | Status |
|---|------|------|--------|
| 8.5.1 | Dashboard Page (stats overview, recent activity, quick actions) | [.task/task_08_16_dashboard_page.md](../.task/task_08_16_dashboard_page.md) | ⬜ Pending |
| 8.5.2 | Vocabulary List Page (search, filter by difficulty, pagination) | [.task/task_08_17_vocabulary_page.md](../.task/task_08_17_vocabulary_page.md) | ⬜ Pending |
| 8.5.3 | Vocabulary Detail / Flashcard View | [.task/task_08_18_vocabulary_detail.md](../.task/task_08_18_vocabulary_detail.md) | ⬜ Pending |
| 8.5.4 | Quiz List Page (filter by difficulty, search) | [.task/task_08_19_quiz_list_page.md](../.task/task_08_19_quiz_list_page.md) | ⬜ Pending |
| 8.5.5 | Quiz Take Page (timer, question navigation, submit) | [.task/task_08_20_quiz_take_page.md](../.task/task_08_20_quiz_take_page.md) | ⬜ Pending |
| 8.5.6 | Quiz Result Page (score, review answers, correct/incorrect) | [.task/task_08_21_quiz_result_page.md](../.task/task_08_21_quiz_result_page.md) | ⬜ Pending |
| 8.5.7 | History Page (learning activity timeline, filter) | [.task/task_08_22_history_page.md](../.task/task_08_22_history_page.md) | ⬜ Pending |
| 8.5.8 | Leaderboard Page (ranking table, period filter, user highlight) | [.task/task_08_23_leaderboard_page.md](../.task/task_08_23_leaderboard_page.md) | ⬜ Pending |

### 8.6 Admin Pages (Optional)

| # | Task | File | Status |
|---|------|------|--------|
| 8.6.1 | Admin Dashboard (user management, content management) | [.task/task_08_24_admin_dashboard.md](../.task/task_08_24_admin_dashboard.md) | ⬜ Pending |
| 8.6.2 | Admin: Create/Edit Quiz Page | [.task/task_08_25_admin_quiz.md](../.task/task_08_25_admin_quiz.md) | ⬜ Pending |
| 8.6.3 | Admin: Manage Vocabulary Page | [.task/task_08_26_admin_vocabulary.md](../.task/task_08_26_admin_vocabulary.md) | ⬜ Pending |

---

## Phase 9: Integration, CORS & Final Testing

| # | Task | File | Status |
|---|------|------|--------|
| 9.1 | Configure CORS in WebAPI for frontend | [.task/task_09_01_configure_cors.md](../.task/task_09_01_configure_cors.md) | ⬜ Pending |
| 9.2 | Setup proxy / environment variables for API URL | [.task/task_09_02_api_proxy.md](../.task/task_09_02_api_proxy.md) | ⬜ Pending |
| 9.3 | End-to-end testing (Register → Login → Take Quiz → View Result → Leaderboard) | [.task/task_09_03_e2e_testing.md](../.task/task_09_03_e2e_testing.md) | ⬜ Pending |
| 9.4 | Responsive design review (mobile, tablet, desktop) | [.task/task_09_04_responsive.md](../.task/task_09_04_responsive.md) | ⬜ Pending |
| 9.5 | Build & deploy preparation | [.task/task_09_05_build_deploy.md](../.task/task_09_05_build_deploy.md) | ⬜ Pending |

---

## Dependencies Between Tasks

```
Phase 1 (Domain) → Phase 2 (Application) → Phase 3 (Infrastructure) → Phase 4 (WebAPI)
                                                                    ↓
Phase 5 (Migration) → Phase 6 (Auth) → Phase 7 (History + Leaderboard)
                                                                    ↓
Phase 8 (Frontend) ← Phase 6 + 7 (APIs ready)
                                                                    ↓
Phase 9 (Integration & Testing)
```

**Lưu ý:**
- Phase 1-4: Hoàn tất (Backend core)
- Phase 5: Đang fix migration (cascade path)
- Phase 6-7: Backend features mới (Auth, History, Leaderboard)
- Phase 8: Frontend (có thể bắt đầu song song sau khi Phase 6 hoàn tất API Auth)
- Phase 9: Integration cuối cùng

---

## Notes

- **Database:** SQL Server (localdb for development)
- **Authentication:** JWT + Refresh Token, BCrypt password hashing
- **Roles:** Admin (quản lý nội dung), User (học tập)
- **Frontend:** ReactJS + TypeScript + Vite + TailwindCSS + React Query + Axios
- **CORS:** WebAPI cần cấu hình CORS cho frontend dev server
- **Auto-grading:** Implemented in QuizResult handler (compare submitted answers with correct answers)
- **Leaderboard:** Tính toán dựa trên tổng điểm, số quiz hoàn thành, điểm trung bình, streak
- **History:** Tự động log khi user submit quiz, xem vocabulary, bookmark word
