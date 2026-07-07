import { useState, useEffect } from 'react'
import { useParams, Link } from 'react-router-dom'
import { ArrowLeft, Award } from 'lucide-react'

export default function QuizResultPage() {
  const { id } = useParams<{ id: string }>()
  const [isLoading, setIsLoading] = useState(true)

  useEffect(() => {
    if (!id) return
    setIsLoading(false)
  }, [id])

  if (isLoading) {
    return (
      <div className="flex justify-center py-12">
        <div className="h-8 w-8 animate-spin rounded-full border-4 border-primary-600 border-t-transparent" />
      </div>
    )
  }

  return (
    <div className="max-w-2xl mx-auto">
      <Link
        to="/quizzes"
        className="mb-4 inline-flex items-center gap-2 text-sm text-gray-600 hover:text-gray-900"
      >
        <ArrowLeft className="h-4 w-4" />
        Back to Quizzes
      </Link>

      <div className="card text-center">
        <div className="mx-auto flex h-16 w-16 items-center justify-center rounded-full bg-primary-100">
          <Award className="h-8 w-8 text-primary-600" />
        </div>
        <h1 className="mt-4 text-2xl font-bold text-gray-900">Quiz Completed!</h1>
        <p className="mt-2 text-gray-600">Here are your results</p>

        <p className="mt-4 text-gray-500">Results will be available shortly.</p>

        <div className="mt-6">
          <Link to="/quizzes" className="btn btn-primary">
            Back to Quizzes
          </Link>
        </div>
      </div>
    </div>
  )
}
