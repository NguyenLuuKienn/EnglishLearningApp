import { useState, useEffect } from 'react'
import { quizService } from '@/services/quizService'
import { Quiz, DifficultyLevel } from '@/types'
import { Plus, Edit, Trash2, X, Copy, FileQuestion } from 'lucide-react'
import { Link } from 'react-router-dom'

export default function AdminQuizPage() {
  const [quizzes, setQuizzes] = useState<Quiz[]>([])
  const [showForm, setShowForm] = useState(false)
  const [editingQuiz, setEditingQuiz] = useState<Quiz | null>(null)
  const [copiedId, setCopiedId] = useState<string | null>(null)
  const [form, setForm] = useState({
    title: '',
    description: '',
    difficulty: 'Beginner' as DifficultyLevel,
    timeLimitMinutes: 30,
  })

  useEffect(() => {
    loadQuizzes()
  }, [])

  const loadQuizzes = () => {
    quizService
      .getAll()
      .then((data) => setQuizzes(data || []))
      .catch((error) => {
        console.error('Failed to load quizzes:', error)
        setQuizzes([])
      })
  }

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault()
    if (editingQuiz) {
      await quizService.update(editingQuiz.id, form)
    } else {
      await quizService.create(form)
    }
    resetForm()
    loadQuizzes()
  }

  const handleEdit = (quiz: Quiz) => {
    setEditingQuiz(quiz)
    setForm({
      title: quiz.title,
      description: quiz.description || '',
      difficulty: quiz.difficulty,
      timeLimitMinutes: quiz.timeLimitMinutes || 30,
    })
    setShowForm(true)
  }

  const handleDelete = async (id: string) => {
    if (!confirm('Are you sure you want to delete this quiz?')) return
    await quizService.delete(id)
    loadQuizzes()
  }

  const copyId = (id: string) => {
    navigator.clipboard.writeText(id)
    setCopiedId(id)
    setTimeout(() => setCopiedId(null), 2000)
  }

  const resetForm = () => {
    setShowForm(false)
    setEditingQuiz(null)
    setForm({ title: '', description: '', difficulty: 'Beginner', timeLimitMinutes: 30 })
  }

  return (
    <div>
      <div className="mb-6 flex items-center justify-between">
        <div>
          <h1 className="text-2xl font-bold text-gray-900">Manage Quizzes</h1>
          <p className="text-gray-600">Create and manage quiz content</p>
        </div>
        <button className="btn btn-primary" onClick={() => setShowForm(true)}>
          <Plus className="mr-2 h-4 w-4" />
          New Quiz
        </button>
      </div>

      {/* Quiz List */}
      {quizzes.length === 0 ? (
        <div className="card text-center">
          <p className="text-gray-500">No quizzes yet. Create one to get started.</p>
        </div>
      ) : (
        <div className="space-y-3">
          {quizzes.map((q) => (
            <div key={q.id} className="card">
              <div className="flex items-center justify-between">
                <div className="flex-1">
                  <h3 className="font-semibold text-gray-900">{q.title}</h3>
                  <p className="text-sm text-gray-500">{q.description}</p>
                  <div className="mt-2 flex items-center gap-3">
                    <span className="badge badge-primary">{q.difficulty}</span>
                    <span className="text-xs text-gray-400">{q.timeLimitMinutes} min</span>
                    <span className="text-xs font-mono text-gray-400">ID: {q.id.slice(0, 8)}...</span>
                  </div>
                </div>
                <div className="flex items-center gap-2">
                  <button
                    className="btn btn-secondary text-xs"
                    onClick={() => copyId(q.id)}
                    title="Copy Quiz ID"
                  >
                    {copiedId === q.id ? 'Copied!' : <Copy className="h-4 w-4" />}
                  </button>
                  <Link
                    to={`/admin/quizzes/${q.id}/questions`}
                    className="btn btn-secondary text-xs gap-1"
                  >
                    <FileQuestion className="h-4 w-4" />
                    Questions
                  </Link>
                  <button className="btn btn-secondary" onClick={() => handleEdit(q)}>
                    <Edit className="h-4 w-4" />
                  </button>
                  <button className="btn btn-danger" onClick={() => handleDelete(q.id)}>
                    <Trash2 className="h-4 w-4" />
                  </button>
                </div>
              </div>
            </div>
          ))}
        </div>
      )}

      {/* Form Modal */}
      {showForm && (
        <div className="fixed inset-0 z-50 flex items-center justify-center bg-black/50 p-4">
          <div className="card w-full max-w-lg">
            <div className="mb-4 flex items-center justify-between">
              <h2 className="text-lg font-semibold text-gray-900">
                {editingQuiz ? 'Edit Quiz' : 'Create New Quiz'}
              </h2>
              <button onClick={resetForm} className="text-gray-400 hover:text-gray-600">
                <X className="h-5 w-5" />
              </button>
            </div>

            <form onSubmit={handleSubmit}>
              <div className="mb-4">
                <label className="mb-1 block text-sm font-medium text-gray-700">Title</label>
                <input
                  type="text"
                  className="input"
                  value={form.title}
                  onChange={(e) => setForm({ ...form, title: e.target.value })}
                  required
                />
              </div>

              <div className="mb-4">
                <label className="mb-1 block text-sm font-medium text-gray-700">Description</label>
                <textarea
                  className="input"
                  rows={3}
                  value={form.description}
                  onChange={(e) => setForm({ ...form, description: e.target.value })}
                />
              </div>

              <div className="mb-4">
                <label className="mb-1 block text-sm font-medium text-gray-700">Difficulty</label>
                <select
                  className="input"
                  value={form.difficulty}
                  onChange={(e) => setForm({ ...form, difficulty: e.target.value as DifficultyLevel })}
                >
                  <option value="Beginner">Beginner</option>
                  <option value="Intermediate">Intermediate</option>
                  <option value="Advanced">Advanced</option>
                </select>
              </div>

              <div className="mb-6">
                <label className="mb-1 block text-sm font-medium text-gray-700">
                  Time Limit (minutes)
                </label>
                <input
                  type="number"
                  className="input"
                  value={form.timeLimitMinutes}
                  onChange={(e) => setForm({ ...form, timeLimitMinutes: parseInt(e.target.value) })}
                  min={1}
                />
              </div>

              <div className="flex justify-end gap-3">
                <button type="button" className="btn btn-secondary" onClick={resetForm}>
                  Cancel
                </button>
                <button type="submit" className="btn btn-primary">
                  {editingQuiz ? 'Update' : 'Create'}
                </button>
              </div>
            </form>
          </div>
        </div>
      )}

      {/* Quiz List */}
      <div className="space-y-3">
        {quizzes.map((q) => (
          <div key={q.id} className="card flex items-center justify-between">
            <div>
              <h3 className="font-semibold text-gray-900">{q.title}</h3>
              <p className="text-sm text-gray-500">{q.description}</p>
            </div>
            <div className="flex items-center gap-2">
              <span className="badge badge-primary">{q.difficulty}</span>
              <button className="btn btn-secondary" onClick={() => handleEdit(q)}>
                <Edit className="h-4 w-4" />
              </button>
              <button className="btn btn-danger" onClick={() => handleDelete(q.id)}>
                <Trash2 className="h-4 w-4" />
              </button>
            </div>
          </div>
        ))}
      </div>
    </div>
  )
}
