import { useState, useEffect } from 'react'
import { useParams, useNavigate } from 'react-router-dom'
import { quizService } from '@/services/quizService'
import { Quiz, Question, QuizAnswer } from '@/types'
import { ArrowLeft, Clock } from 'lucide-react'

export default function QuizTakePage() {
  const { id } = useParams<{ id: string }>()
  const navigate = useNavigate()
  const [quiz, setQuiz] = useState<Quiz | null>(null)
  const [questions, _setQuestions] = useState<Question[]>([])
  const [currentQuestion, setCurrentQuestion] = useState(0)
  const [answers, setAnswers] = useState<Record<string, number>>({})
  const [isLoading, setIsLoading] = useState(true)
  const [submitting, setSubmitting] = useState(false)

  useEffect(() => {
    if (!id) return
    quizService
      .getById(id)
      .then(setQuiz)
      .catch(console.error)
      .finally(() => setIsLoading(false))
  }, [id])

  const handleSelectAnswer = (answerIndex: number) => {
    const qId = questions[currentQuestion]?.id
    if (qId) {
      setAnswers((prev) => ({ ...prev, [qId]: answerIndex }))
    }
  }

  const handleSubmit = async () => {
    if (!id) return
    setSubmitting(true)

    const quizAnswers: QuizAnswer[] = questions.map((q) => ({
      questionId: q.id,
      selectedAnswerIndex: answers[q.id] ?? -1,
      isCorrect: false,
    }))

    try {
      await quizService.submitResult({ quizId: id, answers: quizAnswers })
      // Navigate to result page
      navigate(`/quizzes/${id}/result`)
    } catch (err) {
      console.error('Failed to submit quiz', err)
    } finally {
      setSubmitting(false)
    }
  }

  if (isLoading) {
    return (
      <div className="flex justify-center py-12">
        <div className="h-8 w-8 animate-spin rounded-full border-4 border-primary-600 border-t-transparent" />
      </div>
    )
  }

  if (!quiz || questions.length === 0) {
    return (
      <div className="card text-center">
        <p className="text-gray-600">Quiz not found or no questions available</p>
      </div>
    )
  }

  const question = questions[currentQuestion]
  const progress = ((currentQuestion + 1) / questions.length) * 100

  return (
    <div className="max-w-2xl mx-auto">
      {/* Header */}
      <div className="mb-6 flex items-center justify-between">
        <button
          onClick={() => navigate(-1)}
          className="flex items-center gap-2 text-sm text-gray-600 hover:text-gray-900"
        >
          <ArrowLeft className="h-4 w-4" />
          Back
        </button>
        <div className="flex items-center gap-2 text-sm text-gray-500">
          <Clock className="h-4 w-4" />
          {quiz.timeLimitMinutes ?? 'No'} time limit
        </div>
      </div>

      {/* Progress */}
      <div className="mb-6">
        <div className="mb-2 flex justify-between text-sm text-gray-600">
          <span>
            Question {currentQuestion + 1} of {questions.length}
          </span>
          <span>{Math.round(progress)}% complete</span>
        </div>
        <div className="h-2 w-full rounded-full bg-gray-200">
          <div
            className="h-2 rounded-full bg-primary-600 transition-all"
            style={{ width: `${progress}%` }}
          />
        </div>
      </div>

      {/* Question */}
      <div className="card">
        <h2 className="text-lg font-semibold text-gray-900">{question?.text}</h2>

        <div className="mt-6 space-y-3">
          {question?.choices.map((choice, index) => {
            const isSelected = answers[question.id] === index
            return (
              <button
                key={choice.id}
                onClick={() => handleSelectAnswer(index)}
                className={`flex w-full items-center gap-3 rounded-lg border p-4 text-left transition-colors ${
                  isSelected
                    ? 'border-primary-500 bg-primary-50'
                    : 'border-gray-200 hover:border-gray-300 hover:bg-gray-50'
                }`}
              >
                <div
                  className={`flex h-5 w-5 items-center justify-center rounded-full border ${
                    isSelected ? 'border-primary-500 bg-primary-500' : 'border-gray-300'
                  }`}
                >
                  {isSelected && <div className="h-2 w-2 rounded-full bg-white" />}
                </div>
                <span className="text-gray-700">{choice.text}</span>
              </button>
            )
          })}
        </div>

        {/* Navigation */}
        <div className="mt-6 flex justify-between">
          <button
            className="btn btn-secondary"
            disabled={currentQuestion === 0}
            onClick={() => setCurrentQuestion((q) => q - 1)}
          >
            Previous
          </button>

          {currentQuestion < questions.length - 1 ? (
            <button
              className="btn btn-primary"
              onClick={() => setCurrentQuestion((q) => q + 1)}
            >
              Next
            </button>
          ) : (
            <button
              className="btn btn-success"
              onClick={handleSubmit}
              disabled={submitting || Object.keys(answers).length < questions.length}
            >
              {submitting ? 'Submitting...' : 'Submit Quiz'}
            </button>
          )}
        </div>
      </div>
    </div>
  )
}
