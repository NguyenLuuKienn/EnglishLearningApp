# English Learning App - Implementation Plan

## Overview

Build a complete English Learning System using Clean Architecture + CQRS pattern in .NET 8. Current state: skeleton project with empty stubs — everything needs implementation from scratch.

---

## Current State Analysis

| Layer | Status |
|-------|--------|
| **Domain** | Empty entity stubs (no properties), enums defined as `class` instead of `enum`, no interfaces |
| **Application** | Empty folders, no MediatR, no CQRS, no DTOs, no validators |
| **Infrastructure** | Empty DependencyInjection, no DbContext, no repositories |
| **WebAPI** | Default template only, no JWT, no feature controllers |
| **Cross-cutting** | No project references between layers, no shared NuGet packages |

---

## Architecture

```
Clean Architecture + CQRS
├── Repository Pattern
├── Unit of Work
├── Mediator Pattern (MediatR)
├── SOLID Principles
└── Separation of Concerns
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
| 2.1 | Setup Dependencies | [.task/task_02_01_setup_application_dependencies.md](../.task/task_02_01_setup_application_dependencies.md) | ⬜ Pending |
| 2.2 | Common Classes (Result, PagedResult) | [.task/task_02_02_common_classes.md](../.task/task_02_02_common_classes.md) | ⬜ Pending |
| 2.3 | Create DTOs | [.task/task_02_03_create_dtos.md](../.task/task_02_03_create_dtos.md) | ⬜ Pending |
| 2.4 | CQRS: Vocabulary Features | [.task/task_02_04_cqrs_vocabulary.md](../.task/task_02_04_cqrs_vocabulary.md) | ⬜ Pending |
| 2.5 | CQRS: Quiz Features | [.task/task_02_05_cqrs_quiz.md](../.task/task_02_05_cqrs_quiz.md) | ⬜ Pending |
| 2.6 | CQRS: QuizResult Features | [.task/task_02_06_cqrs_quiz_result.md](../.task/task_02_06_cqrs_quiz_result.md) | ⬜ Pending |
| 2.7 | AutoMapper Profiles | [.task/task_02_07_automapper_profiles.md](../.task/task_02_07_automapper_profiles.md) | ⬜ Pending |
| 2.8 | DependencyInjection | [.task/task_02_08_application_di.md](../.task/task_02_08_application_di.md) | ⬜ Pending |

---

## Phase 3: Infrastructure Layer

| # | Task | File | Status |
|---|------|------|--------|
| 3.1 | Setup Dependencies | [.task/task_03_01_setup_infrastructure_dependencies.md](../.task/task_03_01_setup_infrastructure_dependencies.md) | ⬜ Pending |
| 3.2 | Create DbContext | [.task/task_03_02_create_dbcontext.md](../.task/task_03_02_create_dbcontext.md) | ⬜ Pending |
| 3.3 | Entity Configurations | [.task/task_03_03_entity_configurations.md](../.task/task_03_03_entity_configurations.md) | ⬜ Pending |
| 3.4 | Base Repository | [.task/task_03_04_base_repository.md](../.task/task_03_04_base_repository.md) | ⬜ Pending |
| 3.5 | Specific Repositories | [.task/task_03_05_specific_repositories.md](../.task/task_03_05_specific_repositories.md) | ⬜ Pending |
| 3.6 | Unit of Work | [.task/task_03_06_unit_of_work.md](../.task/task_03_06_unit_of_work.md) | ⬜ Pending |
| 3.7 | DependencyInjection | [.task/task_03_07_infrastructure_di.md](../.task/task_03_07_infrastructure_di.md) | ⬜ Pending |

---

## Phase 4: WebAPI Layer

| # | Task | File | Status |
|---|------|------|--------|
| 4.1 | Setup Dependencies | [.task/task_04_01_setup_webapi_dependencies.md](../.task/task_04_01_setup_webapi_dependencies.md) | ⬜ Pending |
| 4.2 | API Response Models | [.task/task_04_02_api_response_models.md](../.task/task_04_02_api_response_models.md) | ⬜ Pending |
| 4.3 | Request Contracts | [.task/task_04_03_request_contracts.md](../.task/task_04_03_request_contracts.md) | ⬜ Pending |
| 4.4 | Create Controllers | [.task/task_04_04_create_controllers.md](../.task/task_04_04_create_controllers.md) | ⬜ Pending |
| 4.5 | Exception Middleware | [.task/task_04_05_exception_middleware.md](../.task/task_04_05_exception_middleware.md) | ⬜ Pending |
| 4.6 | Update Program.cs | [.task/task_04_06_update_program.cs.md](../.task/task_04_06_update_program.cs.md) | ⬜ Pending |
| 4.7 | Update appsettings.json | [.task/task_04_07_update_appsettings.md](../.task/task_04_07_update_appsettings.md) | ⬜ Pending |
| 4.8 | Cleanup Template Files | [.task/task_04_08_cleanup_template_files.md](../.task/task_04_08_cleanup_template_files.md) | ⬜ Pending |

---

## Phase 5: Database Migration & Seeding

| # | Task | File | Status |
|---|------|------|--------|
| 5.1 | Initial Migration | [.task/task_05_01_initial_migration.md](../.task/task_05_01_initial_migration.md) | ⬜ Pending |
| 5.2 | Data Seeding | [.task/task_05_02_data_seeding.md](../.task/task_05_02_data_seeding.md) | ⬜ Pending |

---

## Phase 6: Final Verification

| # | Task | File | Status |
|---|------|------|--------|
| 6.1 | Solution-Wide Build | [.task/task_06_01_solution_build.md](../.task/task_06_01_solution_build.md) | ⬜ Pending |
| 6.2 | Run & Test API | [.task/task_06_02_run_and_test.md](../.task/task_06_02_run_and_test.md) | ⬜ Pending |

---

## Dependencies Between Tasks

```
Phase 1 (Domain) → Phase 2 (Application) → Phase 3 (Infrastructure) → Phase 4 (WebAPI) → Phase 5 (Migration) → Phase 6 (Verification)
```

Each phase depends on the previous phase being complete. Tasks within a phase can be parallelized if independent.

---

## Notes

- Database: SQL Server (localdb for development)
- Authentication: JWT (Phase 1 scope — can be enhanced later)
- User management: Basic scope for Phase 1 (UserId in QuizResult)
- Auto-grading: Implemented in QuizResult handler (compare submitted answers with correct answers)
