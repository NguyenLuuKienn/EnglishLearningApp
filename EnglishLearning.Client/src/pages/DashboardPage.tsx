import { useAuth } from '@/store/AuthContext'
import { Link } from 'react-router-dom'
import { BookOpen, Award, TrendingUp, Clock } from 'lucide-react'

export default function DashboardPage() {
  const { user } = useAuth()

  const stats = [
    { label: 'Words Learned', value: '0', icon: BookOpen, color: 'primary' },
    { label: 'Quizzes Taken', value: '0', icon: Award, color: 'success' },
    { label: 'Average Score', value: '0%', icon: TrendingUp, color: 'warning' },
    { label: 'Study Streak', value: '0 days', icon: Clock, color: 'danger' },
  ]

  return (
    <div>
      <div className="mb-6">
        <h1 className="text-2xl font-bold text-gray-900">
          Welcome back, {user?.username}! 👋
        </h1>
        <p className="mt-1 text-gray-600">Here&apos;s your learning overview</p>
      </div>

      {/* Stats Grid */}
      <div className="mb-8 grid gap-4 sm:grid-cols-2 lg:grid-cols-4">
        {stats.map(({ label, value, icon: Icon, color }) => (
          <div key={label} className="card">
            <div className="flex items-center gap-4">
              <div
                className={`flex h-12 w-12 items-center justify-center rounded-lg bg-${color}-100`}
              >
                <Icon className={`h-6 w-6 text-${color}-600`} />
              </div>
              <div>
                <p className="text-2xl font-bold text-gray-900">{value}</p>
                <p className="text-sm text-gray-500">{label}</p>
              </div>
            </div>
          </div>
        ))}
      </div>

      {/* Quick Actions */}
      <div className="grid gap-6 lg:grid-cols-2">
        <div className="card">
          <h2 className="mb-4 text-lg font-semibold text-gray-900">Quick Actions</h2>
          <div className="grid gap-3 sm:grid-cols-2">
            <Link to="/vocabulary" className="btn btn-primary">
              Browse Vocabulary
            </Link>
            <Link to="/quizzes" className="btn btn-secondary">
              Take a Quiz
            </Link>
            <Link to="/leaderboard" className="btn btn-secondary">
              Leaderboard
            </Link>
            <Link to="/history" className="btn btn-secondary">
              My History
            </Link>
          </div>
        </div>

        <div className="card">
          <h2 className="mb-4 text-lg font-semibold text-gray-900">Assigned Quizzes</h2>
          <p className="text-sm text-gray-500">No quizzes assigned yet.</p>
          <Link
            to="/quizzes"
            className="mt-3 inline-block text-sm font-medium text-primary-600 hover:text-primary-500"
          >
            View all quizzes →
          </Link>
        </div>
      </div>
    </div>
  )
}
