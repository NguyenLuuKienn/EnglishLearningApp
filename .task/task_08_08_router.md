# Task 8.8: Router Configuration

## Description

Setup React Router with public and protected routes.

## Priority
🔴 Critical — Navigation

## Dependencies
- Task 8.7 (Auth Context)

## Files to Create

| File | Action |
|------|--------|
| `EnglishLearning.Web/src/components/layout/ProtectedRoute.tsx` | Create |

## Files to Edit

| File | Action |
|------|--------|
| `EnglishLearning.Web/src/App.tsx` | Edit |

## Steps

### Step 1: Create ProtectedRoute component
1. Check if user is authenticated
2. Redirect to /login if not authenticated
3. Render children if authenticated

### Step 2: Configure routes in App.tsx
1. Public routes: /login, /register
2. Protected routes: /dashboard, /vocabulary, /quiz, /history, /leaderboard, /profile

## Expected Code

```typescript
// components/layout/ProtectedRoute.tsx
import { Navigate, useLocation } from 'react-router-dom';
import useAuth from '../../hooks/useAuth';

interface ProtectedRouteProps {
  children: React.ReactNode;
}

const ProtectedRoute: React.FC<ProtectedRouteProps> = ({ children }) => {
  const { isAuthenticated, isLoading } = useAuth();
  const location = useLocation();

  if (isLoading) {
    return (
      <div className="flex items-center justify-center min-h-screen">
        <div className="animate-spin rounded-full h-12 w-12 border-b-2 border-blue-600"></div>
      </div>
    );
  }

  if (!isAuthenticated) {
    return <Navigate to="/login" state={{ from: location }} replace />;
  }

  return <>{children}</>;
};

export default ProtectedRoute;

// App.tsx
import { BrowserRouter, Routes, Route, Navigate } from 'react-router-dom';
import { QueryClient, QueryClientProvider } from '@tanstack/react-query';
import { AuthProvider } from './context/AuthContext';
import ProtectedRoute from './components/layout/ProtectedRoute';

// Pages
import LoginPage from './pages/LoginPage';
import RegisterPage from './pages/RegisterPage';
import DashboardPage from './pages/DashboardPage';
import VocabularyPage from './pages/VocabularyPage';
import QuizListPage from './pages/QuizListPage';
import QuizTakePage from './pages/QuizTakePage';
import QuizResultPage from './pages/QuizResultPage';
import HistoryPage from './pages/HistoryPage';
import LeaderboardPage from './pages/LeaderboardPage';
import ProfilePage from './pages/ProfilePage';

const queryClient = new QueryClient();

function App() {
  return (
    <QueryClientProvider client={queryClient}>
      <AuthProvider>
        <BrowserRouter>
          <Routes>
            <Route path="/login" element={<LoginPage />} />
            <Route path="/register" element={<RegisterPage />} />
            <Route path="/" element={<Navigate to="/dashboard" replace />} />
            <Route path="/dashboard" element={
              <ProtectedRoute><DashboardPage /></ProtectedRoute>
            } />
            <Route path="/vocabulary" element={
              <ProtectedRoute><VocabularyPage /></ProtectedRoute>
            } />
            <Route path="/quiz" element={
              <ProtectedRoute><QuizListPage /></ProtectedRoute>
            } />
            <Route path="/quiz/:id" element={
              <ProtectedRoute><QuizTakePage /></ProtectedRoute>
            } />
            <Route path="/quiz/:id/result" element={
              <ProtectedRoute><QuizResultPage /></ProtectedRoute>
            } />
            <Route path="/history" element={
              <ProtectedRoute><HistoryPage /></ProtectedRoute>
            } />
            <Route path="/leaderboard" element={
              <ProtectedRoute><LeaderboardPage /></ProtectedRoute>
            } />
            <Route path="/profile" element={
              <ProtectedRoute><ProfilePage /></ProtectedRoute>
            } />
          </Routes>
        </BrowserRouter>
      </AuthProvider>
    </QueryClientProvider>
  );
}

export default App;
```

## Verification

- [ ] Router configured with all routes
- [ ] ProtectedRoute redirects to login
- [ ] Public routes accessible without auth

## Acceptance Criteria

- [ ] `ProtectedRoute` component checks authentication
- [ ] Public routes: /login, /register
- [ ] Protected routes: /dashboard, /vocabulary, /quiz, /history, /leaderboard, /profile
- [ ] Root path redirects to /dashboard
- [ ] QueryClientProvider wraps app
- [ ] AuthProvider wraps app
