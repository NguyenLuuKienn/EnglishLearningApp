import { useState, useEffect } from 'react'
import { Link } from 'react-router-dom'
import { useAuth } from '@/store/AuthContext'
import { quizService } from '@/services/quizService'
import { QuizAssignment, AssignmentStatus } from '@/types'
import { ClipboardList, Clock, CheckCircle, XCircle, Calendar } from 'lucide-react'

export default function QuizListPage() {
  const { user } = useAuth()
  const [assignments, setAssignments] = useState<QuizAssignment[]>([])
  const [isLoading, setIsLoading] = useState(true)

  useEffect(() => {
    if (!user) return
    quizService
      .getUserAssignmentsByUserId(user.id)
      .then((data) => setAssignments(data || []))
      .catch((error) => {
        console.error('Failed to load quiz assignments:', error)
        setAssignments([])
      })
      .finally(() => setIsLoading(false))
  }, [user])

  const statusConfig: Record<AssignmentStatus, { label: string; badge: string; icon: any }> = {
    Scheduled: { label: 'Scheduled', badge: 'badge-primary', icon: Calendar },
    Active: { label: 'Active', badge: 'badge-success', icon: Clock },
    Completed: { label: 'Completed', badge: 'badge-warning', icon: CheckCircle },
    Cancelled: { label: 'Cancelled', badge: 'badge-danger', icon: XCircle },
  }

  return (
    <div>
      <div className="mb-6">
        <h1 className="text-2xl font-bold text-gray-900">My Quizzes</h1>
        <p className="text-gray-600">Your assigned quizzes and their status</p>
      </div>

      {isLoading ? (
        <div className="flex justify-center py-12">
          <div className="h-8 w-8 animate-spin rounded-full border-4 border-primary-600 border-t-transparent" />
        </div>
      ) : assignments.length === 0 ? (
        <div className="card text-center">
          <ClipboardList className="mx-auto h-12 w-12 text-gray-300" />
          <p className="mt-4 text-gray-600">No quizzes assigned yet</p>
        </div>
      ) : (
        <div className="space-y-4">
          {assignments.map((a) => {
            const config = statusConfig[a.status]
            const Icon = config.icon

            return (
              <div key={a.id} className="card">
                <div className="flex items-center justify-between">
                  <div className="flex items-center gap-4">
                    <div className="flex h-10 w-10 items-center justify-center rounded-lg bg-primary-100">
                      <Icon className="h-5 w-5 text-primary-600" />
                    </div>
                    <div>
                      <h3 className="font-semibold text-gray-900">{a.quizTitle}</h3>
                      <p className="text-sm text-gray-500">
                        {new Date(a.startTime).toLocaleDateString()} —{' '}
                        {new Date(a.endTime).toLocaleDateString()}
                      </p>
                    </div>
                  </div>
                  <div className="flex items-center gap-3">
                    <span className={`badge ${config.badge}`}>{config.label}</span>
                    {a.status === 'Active' && (
                      <Link to={`/quizzes/${a.quizId}/take`} className="btn btn-primary">
                        Start Quiz
                      </Link>
                    )}
                    {a.status === 'Completed' && (
                      <Link to={`/quizzes/${a.quizId}/result`} className="btn btn-secondary">
                        View Result
                      </Link>
                    )}
                  </div>
                </div>
              </div>
            )
          })}
        </div>
      )}
    </div>
  )
}
