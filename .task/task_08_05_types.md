# Task 8.5: TypeScript Types

## Description

Create TypeScript interfaces/types matching backend DTOs.

## Priority
🔴 Critical — Type safety

## Dependencies
- Task 8.4 (Folder structure)

## Files to Create

| File | Action |
|------|--------|
| `EnglishLearning.Web/src/types/api.ts` | Create |
| `EnglishLearning.Web/src/types/auth.ts` | Create |
| `EnglishLearning.Web/src/types/vocabulary.ts` | Create |
| `EnglishLearning.Web/src/types/quiz.ts` | Create |
| `EnglishLearning.Web/src/types/history.ts` | Create |
| `EnglishLearning.Web/src/types/leaderboard.ts` | Create |

## Steps

### Step 1: Create API response types
1. `ApiResponse<T>`, `PagedResponse<T>`, `PagedResult<T>`

### Step 2: Create auth types
1. `User`, `Token`, `LoginRequest`, `RegisterRequest`

### Step 3: Create domain types
1. `Vocabulary`, `Quiz`, `QuizResult`, `LearningHistory`, `Leaderboard`

## Expected Code

```typescript
// types/api.ts
export interface ApiResponse<T> {
  success: boolean;
  message: string;
  data: T;
  errors?: string[];
}

export interface PagedResponse<T> extends ApiResponse<T[]> {
  pageNumber: number;
  pageSize: number;
  totalRecords: number;
  totalPages: number;
}

// types/auth.ts
export interface User {
  id: string;
  username: string;
  email: string;
  role: 'User' | 'Admin';
  avatarUrl?: string;
  createdAt: string;
}

export interface Token {
  accessToken: string;
  refreshToken: string;
  expiresIn: number;
}

export interface LoginRequest {
  username: string;
  password: string;
}

export interface RegisterRequest {
  username: string;
  email: string;
  password: string;
}

// types/vocabulary.ts
export interface Vocabulary {
  id: string;
  word: string;
  definition: string;
  example?: string;
  partOfSpeech?: string;
  difficulty: number;
}

// types/quiz.ts
export interface Quiz {
  id: string;
  title: string;
  description?: string;
  difficulty: number;
  timeLimitMinutes: number;
  passingScore: number;
  questions: Question[];
}

export interface Question {
  id: string;
  questionText: string;
  questionType: number;
  difficulty: number;
  correctAnswer?: string;
  choices: Choice[];
}

export interface Choice {
  id: string;
  choiceText: string;
  isCorrect: boolean;
}

export interface QuizResult {
  id: string;
  quizId: string;
  userId: string;
  score: number;
  totalQuestions: number;
  correctAnswers: number;
  durationMinutes: number;
  completedAt: string;
}

// types/history.ts
export interface LearningHistory {
  id: string;
  userId: string;
  actionType: number;
  targetId: string;
  details?: string;
  score?: number;
  createdAt: string;
}

// types/leaderboard.ts
export interface Leaderboard {
  id: string;
  userId: string;
  username: string;
  totalScore: number;
  quizzesCompleted: number;
  averageScore: number;
  streak: number;
  rank: number;
}
```

## Verification

- [ ] All types match backend DTOs
- [ ] TypeScript compiles without errors

## Acceptance Criteria

- [ ] `ApiResponse<T>` and `PagedResponse<T>` defined
- [ ] `User`, `Token`, `LoginRequest`, `RegisterRequest` defined
- [ ] `Vocabulary`, `Quiz`, `Question`, `Choice`, `QuizResult` defined
- [ ] `LearningHistory`, `Leaderboard` defined
- [ ] All types in `src/types/` folder
