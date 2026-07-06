# Task 8.6: API Service Layer

## Description

Create Axios instance with interceptors for auth token management and API service functions.

## Priority
🔴 Critical — HTTP communication with backend

## Dependencies
- Task 8.3 (Dependencies installed), Task 8.5 (Types)

## Files to Create

| File | Action |
|------|--------|
| `EnglishLearning.Web/src/services/api.ts` | Create |
| `EnglishLearning.Web/src/services/auth.service.ts` | Create |
| `EnglishLearning.Web/src/services/vocabulary.service.ts` | Create |
| `EnglishLearning.Web/src/services/quiz.service.ts` | Create |
| `EnglishLearning.Web/src/services/history.service.ts` | Create |
| `EnglishLearning.Web/src/services/leaderboard.service.ts` | Create |

## Steps

### Step 1: Create Axios instance
1. Base URL from env variable
2. Request interceptor: add auth token
3. Response interceptor: handle 401 (token expired)

### Step 2: Create service functions
1. Auth: login, register, refreshToken, getProfile
2. Vocabulary: getAll, getById
3. Quiz: getAll, getById, submitResult
4. History: getUserHistory
5. Leaderboard: getLeaderboard, getUserRank

## Expected Code

```typescript
// services/api.ts
import axios from 'axios';

const api = axios.create({
  baseURL: import.meta.env.VITE_API_URL || 'http://localhost:5055/api',
  headers: {
    'Content-Type': 'application/json',
  },
});

// Request interceptor - add auth token
api.interceptors.request.use((config) => {
  const token = localStorage.getItem('accessToken');
  if (token) {
    config.headers.Authorization = `Bearer ${token}`;
  }
  return config;
});

// Response interceptor - handle 401
api.interceptors.response.use(
  (response) => response,
  async (error) => {
    if (error.response?.status === 401) {
      // Try to refresh token
      const refreshToken = localStorage.getItem('refreshToken');
      if (refreshToken) {
        try {
          const response = await axios.post(`${import.meta.env.VITE_API_URL}/api/auth/refresh-token`, {
            accessToken: localStorage.getItem('accessToken'),
            refreshToken,
          });
          const newTokens = response.data.data;
          localStorage.setItem('accessToken', newTokens.accessToken);
          localStorage.setItem('refreshToken', newTokens.refreshToken);
          error.config.headers.Authorization = `Bearer ${newTokens.accessToken}`;
          return api(error.config);
        } catch {
          localStorage.clear();
          window.location.href = '/login';
        }
      } else {
        localStorage.clear();
        window.location.href = '/login';
      }
    }
    return Promise.reject(error);
  }
);

export default api;

// services/auth.service.ts
import api from './api';
import { LoginRequest, RegisterRequest, User, Token } from '../types/auth';
import { ApiResponse } from '../types/api';

export const authService = {
  login: async (data: LoginRequest): Promise<ApiResponse<Token>> => {
    const response = await api.post('/auth/login', data);
    return response.data;
  },
  register: async (data: RegisterRequest): Promise<ApiResponse<string>> => {
    const response = await api.post('/auth/register', data);
    return response.data;
  },
  getProfile: async (): Promise<ApiResponse<User>> => {
    const response = await api.get('/auth/profile');
    return response.data;
  },
};

// services/vocabulary.service.ts
import api from './api';
import { Vocabulary } from '../types/vocabulary';
import { ApiResponse, PagedResponse } from '../types/api';

export const vocabularyService = {
  getAll: async (pageNumber = 1, pageSize = 10, difficulty?: number): Promise<PagedResponse<Vocabulary>> => {
    const response = await api.get('/vocabularies', { params: { pageNumber, pageSize, difficulty } });
    return response.data;
  },
  getById: async (id: string): Promise<ApiResponse<Vocabulary>> => {
    const response = await api.get(`/vocabularies/${id}`);
    return response.data;
  },
};

// services/quiz.service.ts
import api from './api';
import { Quiz, QuizResult } from '../types/quiz';
import { ApiResponse, PagedResponse } from '../types/api';

export const quizService = {
  getAll: async (pageNumber = 1, pageSize = 10, difficulty?: number): Promise<PagedResponse<Quiz>> => {
    const response = await api.get('/quizzes', { params: { pageNumber, pageSize, difficulty } });
    return response.data;
  },
  getById: async (id: string): Promise<ApiResponse<Quiz>> => {
    const response = await api.get(`/quizzes/${id}`);
    return response.data;
  },
  submitResult: async (data: any): Promise<ApiResponse<QuizResult>> => {
    const response = await api.post('/quizresults/submit', data);
    return response.data;
  },
};

// services/history.service.ts
import api from './api';
import { LearningHistory } from '../types/history';
import { PagedResponse } from '../types/api';

export const historyService = {
  getUserHistory: async (userId: string, pageNumber = 1, pageSize = 10): Promise<PagedResponse<LearningHistory>> => {
    const response = await api.get(`/history/user/${userId}`, { params: { pageNumber, pageSize } });
    return response.data;
  },
};

// services/leaderboard.service.ts
import api from './api';
import { Leaderboard } from '../types/leaderboard';
import { ApiResponse } from '../types/api';

export const leaderboardService = {
  getLeaderboard: async (count = 100): Promise<ApiResponse<Leaderboard[]>> => {
    const response = await api.get('/leaderboard', { params: { count } });
    return response.data;
  },
  getUserRank: async (userId: string): Promise<ApiResponse<number>> => {
    const response = await api.get(`/leaderboard/user/${userId}/rank`);
    return response.data;
  },
};
```

## Verification

- [ ] Axios instance created with interceptors
- [ ] All service functions defined
- [ ] TypeScript compiles without errors

## Acceptance Criteria

- [ ] Axios instance with base URL from env
- [ ] Request interceptor adds auth token
- [ ] Response interceptor handles 401 with token refresh
- [ ] Auth service: login, register, getProfile
- [ ] Vocabulary service: getAll, getById
- [ ] Quiz service: getAll, getById, submitResult
- [ ] History service: getUserHistory
- [ ] Leaderboard service: getLeaderboard, getUserRank
