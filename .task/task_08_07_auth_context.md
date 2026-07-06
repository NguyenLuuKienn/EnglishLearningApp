# Task 8.7: Auth Context + Custom Hooks

## Description

Create AuthContext for user state management and custom hooks (useAuth, useApi).

## Priority
🔴 Critical — Auth state management

## Dependencies
- Task 8.6 (API services)

## Files to Create

| File | Action |
|------|--------|
| `EnglishLearning.Web/src/context/AuthContext.tsx` | Create |
| `EnglishLearning.Web/src/hooks/useAuth.ts` | Create |
| `EnglishLearning.Web/src/hooks/useGetQuery.ts` | Create |

## Steps

### Step 1: Create AuthContext
1. State: user, accessToken, isLoading, isAuthenticated
2. Actions: login, logout, register
3. Provider component

### Step 2: Create custom hooks
1. `useAuth` — access auth context
2. `useGetQuery` — wrapper for React Query useQuery

## Expected Code

```typescript
// context/AuthContext.tsx
import React, { createContext, useContext, useState, useEffect, ReactNode } from 'react';
import { User, Token } from '../types/auth';
import { authService } from '../services/auth.service';

interface AuthContextType {
  user: User | null;
  accessToken: string | null;
  isLoading: boolean;
  isAuthenticated: boolean;
  login: (username: string, password: string) => Promise<void>;
  register: (username: string, email: string, password: string) => Promise<void>;
  logout: () => void;
}

const AuthContext = createContext<AuthContextType | undefined>(undefined);

export const AuthProvider: React.FC<{ children: ReactNode }> = ({ children }) => {
  const [user, setUser] = useState<User | null>(null);
  const [accessToken, setAccessToken] = useState<string | null>(localStorage.getItem('accessToken'));
  const [isLoading, setIsLoading] = useState(true);

  useEffect(() => {
    if (accessToken) {
      authService.getProfile()
        .then((response) => {
          if (response.success) {
            setUser(response.data);
          }
        })
        .finally(() => setIsLoading(false));
    } else {
      setIsLoading(false);
    }
  }, [accessToken]);

  const login = async (username: string, password: string) => {
    const response = await authService.login({ username, password });
    if (response.success) {
      const tokens: Token = response.data;
      localStorage.setItem('accessToken', tokens.accessToken);
      localStorage.setItem('refreshToken', tokens.refreshToken);
      setAccessToken(tokens.accessToken);
    }
  };

  const register = async (username: string, email: string, password: string) => {
    await authService.register({ username, email, password });
  };

  const logout = () => {
    localStorage.clear();
    setAccessToken(null);
    setUser(null);
  };

  return (
    <AuthContext.Provider value={{
      user, accessToken, isLoading, isAuthenticated: !!user, login, register, logout
    }}>
      {children}
    </AuthContext.Provider>
  );
};

export default AuthContext;

// hooks/useAuth.ts
import { useContext } from 'react';
import AuthContext from '../context/AuthContext';

const useAuth = () => {
  const context = useContext(AuthContext);
  if (context === undefined) {
    throw new Error('useAuth must be used within an AuthProvider');
  }
  return context;
};

export default useAuth;
```

## Verification

- [ ] AuthContext provides user state and actions
- [ ] useAuth hook works correctly
- [ ] TypeScript compiles without errors

## Acceptance Criteria

- [ ] `AuthContext` with user, accessToken, isLoading, isAuthenticated
- [ ] `login`, `register`, `logout` actions
- [ ] `useAuth` custom hook
- [ ] Token stored in localStorage
- [ ] Auto-load user profile on mount
