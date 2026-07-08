import { Routes, Route, Navigate } from 'react-router-dom'
import { AuthProvider } from './store/AuthContext'
import { ProtectedRoute, AdminRoute } from './components/layout/ProtectedRoute'

// Public pages
import LoginPage from './pages/auth/LoginPage'
import RegisterPage from './pages/auth/RegisterPage'

// Protected pages
import DashboardPage from './pages/DashboardPage'
import VocabularyPage from './pages/vocabulary/VocabularyPage'
import VocabularyDetailPage from './pages/vocabulary/VocabularyDetailPage'
import QuizListPage from './pages/quiz/QuizListPage'
import QuizTakePage from './pages/quiz/QuizTakePage'
import QuizResultPage from './pages/quiz/QuizResultPage'
import HistoryPage from './pages/HistoryPage'
import LeaderboardPage from './pages/LeaderboardPage'
import NotificationsPage from './pages/NotificationsPage'
import ProfilePage from './pages/ProfilePage'

// Admin pages
import AdminDashboardPage from './pages/admin/AdminDashboardPage'
import AdminQuizPage from './pages/admin/AdminQuizPage'
import AdminAssignQuizPage from './pages/admin/AdminAssignQuizPage'
import AdminVocabularyPage from './pages/admin/AdminVocabularyPage'
import AdminQuestionsPage from './pages/admin/AdminQuestionsPage'

function App() {
  return (
    <AuthProvider>
      <Routes>
        {/* Public routes */}
        <Route path="/login" element={<LoginPage />} />
        <Route path="/register" element={<RegisterPage />} />

        {/* Protected routes */}
        <Route element={<ProtectedRoute />}>
          <Route path="/" element={<DashboardPage />} />
          <Route path="/vocabulary" element={<VocabularyPage />} />
          <Route path="/vocabulary/:id" element={<VocabularyDetailPage />} />
          <Route path="/quizzes" element={<QuizListPage />} />
          <Route path="/quizzes/:id/take" element={<QuizTakePage />} />
          <Route path="/quizzes/:id/result" element={<QuizResultPage />} />
          <Route path="/history" element={<HistoryPage />} />
          <Route path="/leaderboard" element={<LeaderboardPage />} />
          <Route path="/notifications" element={<NotificationsPage />} />
          <Route path="/profile" element={<ProfilePage />} />
        </Route>

        {/* Admin routes — requires Admin or Teacher role */}
        <Route element={<AdminRoute />}>
          <Route path="/admin" element={<AdminDashboardPage />} />
          <Route path="/admin/quizzes" element={<AdminQuizPage />} />
          <Route path="/admin/quizzes/:quizId/questions" element={<AdminQuestionsPage />} />
          <Route path="/admin/quizzes/assign" element={<AdminAssignQuizPage />} />
          <Route path="/admin/vocabulary" element={<AdminVocabularyPage />} />
        </Route>

        {/* Fallback */}
        <Route path="*" element={<Navigate to="/" replace />} />
      </Routes>
    </AuthProvider>
  )
}

export default App
