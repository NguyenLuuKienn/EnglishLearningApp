import { useState, useEffect } from 'react'
import { useParams, useNavigate, Link } from 'react-router-dom'
import { quizService } from '@/services/quizService'
import { DifficultyLevel, QuestionType } from '@/types'
import { ArrowLeft, Plus, Edit, Trash2, X, CheckCircle, XCircle } from 'lucide-react'

interface Choice {
  id?: string
  choiceText: string
  isCorrect: boolean
}

interface QuestionForm {
  questionText: string
  questionType: QuestionType
  difficulty: DifficultyLevel
  correctAnswer?: string
  choices: Choice[]
}

const emptyForm: QuestionForm = {
  questionText: '',
  questionType: 'MultipleChoice',
  difficulty: 'Beginner',
  choices: [
    { choiceText: '', isCorrect: false },
    { choiceText: '', isCorrect: false },
  ],
}

export default function AdminQuestionsPage() {
  const { quizId } = useParams<{ quizId: string }>()
  const navigate = useNavigate()
  const [quiz, setQuiz] = useState<any>(null)
  const [questions, setQuestions] = useState<any[]>([])
  const [showForm, setShowForm] = useState(false)
  const [editingQuestion, setEditingQuestion] = useState<any>(null)
  const [form, setForm] = useState<QuestionForm>({ ...emptyForm })
  const [isLoading, setIsLoading] = useState(true)

  useEffect(() => {
    if (!quizId) return
    loadQuiz()
  }, [quizId])

  const loadQuiz = async () => {
    try {
      const data = await quizService.getById(quizId!)
      setQuiz(data)
      setQuestions(data.questions || [])
    } catch {
      setQuestions([])
    } finally {
      setIsLoading(false)
    }
  }

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault()
    if (!quizId) return

    if (editingQuestion) {
      await quizService.updateQuestion(quizId, editingQuestion.id, form)
    } else {
      await quizService.addQuestion(quizId, form)
    }
    resetForm()
    loadQuiz()
  }

  const handleEdit = (q: any) => {
    setEditingQuestion(q)
    setForm({
      questionText: q.questionText,
      questionType: q.questionType,
      difficulty: q.difficulty,
      correctAnswer: q.correctAnswer,
      choices: q.choices?.map((c: any) => ({
        id: c.id,
        choiceText: c.choiceText,
        isCorrect: c.isCorrect,
      })) || [{ choiceText: '', isCorrect: false }, { choiceText: '', isCorrect: false }],
    })
    setShowForm(true)
  }

  const handleDelete = async (questionId: string) => {
    if (!confirm('Are you sure you want to delete this question?') || !quizId) return
    await quizService.deleteQuestion(quizId, questionId)
    loadQuiz()
  }

  const addChoice = () => {
    setForm({ ...form, choices: [...form.choices, { choiceText: '', isCorrect: false }] })
  }

  const removeChoice = (index: number) => {
    if (form.choices.length <= 2) return
    setForm({ ...form, choices: form.choices.filter((_, i) => i !== index) })
  }

  const updateChoice = (index: number, field: keyof Choice, value: string | boolean) => {
    const newChoices = [...form.choices]
    newChoices[index] = { ...newChoices[index], [field]: value }
    setForm({ ...form, choices: newChoices })
  }

  const resetForm = () => {
    setShowForm(false)
    setEditingQuestion(null)
    setForm({ ...emptyForm })
  }

  if (isLoading) {
    return (
      <div className="flex justify-center py-12">
        <div className="h-8 w-8 animate-spin rounded-full border-4 border-primary-600 border-t-transparent" />
      </div>
    )
  }

  return (
    <div>
      <div className="mb-6">
        <Link
          to="/admin/quizzes"
          className="mb-4 inline-flex items-center gap-2 text-sm text-gray-600 hover:text-gray-900"
        >
          <ArrowLeft className="h-4 w-4" />
          Back to Quizzes
        </Link>
        <div className="flex items-center justify-between">
          <div>
            <h1 className="text-2xl font-bold text-gray-900">
              Questions — {quiz?.title || 'Quiz'}
            </h1>
            <p className="text-gray-600">Manage questions and choices for this quiz</p>
          </div>
          <button className="btn btn-primary" onClick={() => setShowForm(true)}>
            <Plus className="mr-2 h-4 w-4" />
            New Question
          </button>
        </div>
      </div>

      {/* Question Form Modal */}
      {showForm && (
        <div className="fixed inset-0 z-50 flex items-center justify-center bg-black/50 p-4">
          <div className="card w-full max-w-2xl max-h-[90vh] overflow-y-auto">
            <div className="mb-4 flex items-center justify-between">
              <h2 className="text-lg font-semibold text-gray-900">
                {editingQuestion ? 'Edit Question' : 'Add New Question'}
              </h2>
              <button onClick={resetForm} className="text-gray-400 hover:text-gray-600">
                <X className="h-5 w-5" />
              </button>
            </div>

            <form onSubmit={handleSubmit}>
              <div className="mb-4">
                <label className="mb-1 block text-sm font-medium text-gray-700">Question Text</label>
                <textarea
                  className="input"
                  rows={3}
                  value={form.questionText}
                  onChange={(e) => setForm({ ...form, questionText: e.target.value })}
                  required
                />
              </div>

              <div className="grid grid-cols-2 gap-4 mb-4">
                <div>
                  <label className="mb-1 block text-sm font-medium text-gray-700">Type</label>
                  <select
                    className="input"
                    value={form.questionType}
                    onChange={(e) => setForm({ ...form, questionType: e.target.value as QuestionType })}
                  >
                    <option value="MultipleChoice">Multiple Choice</option>
                    <option value="TrueFalse">True/False</option>
                    <option value="FillInBlank">Fill in Blank</option>
                  </select>
                </div>
                <div>
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
              </div>

              <div className="mb-6">
                <label className="mb-2 block text-sm font-medium text-gray-700">Choices</label>
                {form.choices.map((choice, index) => (
                  <div key={index} className="mb-2 flex items-center gap-2">
                    <input
                      type="radio"
                      name="correctAnswer"
                      checked={choice.isCorrect}
                      onChange={() => {
                        const newChoices = form.choices.map((c, i) =>
                          i === index ? { ...c, isCorrect: true } : { ...c, isCorrect: false }
                        )
                        setForm({ ...form, choices: newChoices })
                      }}
                      className="h-4 w-4 text-primary-600"
                    />
                    <input
                      type="text"
                      className="input flex-1"
                      placeholder={`Choice ${index + 1}`}
                      value={choice.choiceText}
                      onChange={(e) => updateChoice(index, 'choiceText', e.target.value)}
                      required
                    />
                    {form.choices.length > 2 && (
                      <button
                        type="button"
                        className="p-2 text-gray-400 hover:text-danger-600"
                        onClick={() => removeChoice(index)}
                      >
                        <XCircle className="h-4 w-4" />
                      </button>
                    )}
                  </div>
                ))}
                <button type="button" className="btn btn-secondary text-xs mt-2" onClick={addChoice}>
                  <Plus className="mr-1 h-3 w-3" /> Add Choice
                </button>
              </div>

              <div className="flex justify-end gap-3">
                <button type="button" className="btn btn-secondary" onClick={resetForm}>
                  Cancel
                </button>
                <button type="submit" className="btn btn-primary">
                  {editingQuestion ? 'Update' : 'Add'} Question
                </button>
              </div>
            </form>
          </div>
        </div>
      )}

      {/* Questions List */}
      {questions.length === 0 ? (
        <div className="card text-center">
          <p className="text-gray-500">No questions yet. Add questions to this quiz.</p>
        </div>
      ) : (
        <div className="space-y-4">
          {questions.map((q, index) => (
            <div key={q.id} className="card">
              <div className="flex items-start justify-between">
                <div className="flex-1">
                  <div className="flex items-center gap-2">
                    <span className="flex h-6 w-6 items-center justify-center rounded-full bg-primary-100 text-xs font-medium text-primary-700">
                      {index + 1}
                    </span>
                    <h3 className="font-medium text-gray-900">{q.questionText}</h3>
                  </div>
                  <div className="mt-2 ml-8 flex items-center gap-3">
                    <span className="badge badge-primary">{q.questionType}</span>
                    <span className="badge badge-warning">{q.difficulty}</span>
                  </div>
                  <div className="mt-3 ml-8 space-y-1">
                    {q.choices?.map((c: any) => (
                      <div key={c.id} className="flex items-center gap-2 text-sm">
                        {c.isCorrect ? (
                          <CheckCircle className="h-4 w-4 text-success-600" />
                        ) : (
                          <XCircle className="h-4 w-4 text-gray-300" />
                        )}
                        <span className={c.isCorrect ? 'text-success-700 font-medium' : 'text-gray-600'}>
                          {c.choiceText}
                        </span>
                      </div>
                    ))}
                  </div>
                </div>
                <div className="flex items-center gap-2 ml-4">
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
    </div>
  )
}
