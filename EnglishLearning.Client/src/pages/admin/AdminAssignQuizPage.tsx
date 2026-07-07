import { useState } from 'react'
import { quizService } from '@/services/quizService'
import { UserRole } from '@/types'
import { Plus, X } from 'lucide-react'

export default function AdminAssignQuizPage() {
  const [showForm, setShowForm] = useState(false)
  const [form, setForm] = useState({
    quizId: '',
    targetRole: '' as UserRole | '',
    targetUserId: '',
    startTime: '',
    endTime: '',
  })

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault()
    await quizService.assignQuiz({
      quizId: form.quizId,
      targetRole: form.targetRole || undefined,
      targetUserId: form.targetUserId || undefined,
      startTime: form.startTime,
      endTime: form.endTime,
    })
    setShowForm(false)
    setForm({ quizId: '', targetRole: '', targetUserId: '', startTime: '', endTime: '' })
  }

  return (
    <div>
      <div className="mb-6 flex items-center justify-between">
        <div>
          <h1 className="text-2xl font-bold text-gray-900">Assign Quizzes</h1>
          <p className="text-gray-600">Assign quizzes to students or roles</p>
        </div>
        <button className="btn btn-primary" onClick={() => setShowForm(true)}>
          <Plus className="mr-2 h-4 w-4" />
          New Assignment
        </button>
      </div>

      {/* Assignment Form */}
      {showForm && (
        <div className="fixed inset-0 z-50 flex items-center justify-center bg-black/50 p-4">
          <div className="card w-full max-w-lg">
            <div className="mb-4 flex items-center justify-between">
              <h2 className="text-lg font-semibold text-gray-900">Assign Quiz</h2>
              <button onClick={() => setShowForm(false)} className="text-gray-400 hover:text-gray-600">
                <X className="h-5 w-5" />
              </button>
            </div>

            <form onSubmit={handleSubmit}>
              <div className="mb-4">
                <label className="mb-1 block text-sm font-medium text-gray-700">Quiz ID</label>
                <input
                  type="text"
                  className="input"
                  placeholder="Enter quiz UUID"
                  value={form.quizId}
                  onChange={(e) => setForm({ ...form, quizId: e.target.value })}
                  required
                />
              </div>

              <div className="mb-4">
                <label className="mb-1 block text-sm font-medium text-gray-700">Target Role</label>
                <select
                  className="input"
                  value={form.targetRole}
                  onChange={(e) => setForm({ ...form, targetRole: e.target.value as UserRole | '' })}
                >
                  <option value="">All Users</option>
                  <option value="Student">Students</option>
                  <option value="Teacher">Teachers</option>
                </select>
              </div>

              <div className="mb-4">
                <label className="mb-1 block text-sm font-medium text-gray-700">
                  Start Time
                </label>
                <input
                  type="datetime-local"
                  className="input"
                  value={form.startTime}
                  onChange={(e) => setForm({ ...form, startTime: e.target.value })}
                  required
                />
              </div>

              <div className="mb-6">
                <label className="mb-1 block text-sm font-medium text-gray-700">
                  End Time
                </label>
                <input
                  type="datetime-local"
                  className="input"
                  value={form.endTime}
                  onChange={(e) => setForm({ ...form, endTime: e.target.value })}
                  required
                />
              </div>

              <div className="flex justify-end gap-3">
                <button type="button" className="btn btn-secondary" onClick={() => setShowForm(false)}>
                  Cancel
                </button>
                <button type="submit" className="btn btn-primary">
                  Assign
                </button>
              </div>
            </form>
          </div>
        </div>
      )}

      <div className="card text-center">
        <p className="text-gray-500">Click &quot;New Assignment&quot; to assign a quiz</p>
      </div>
    </div>
  )
}
