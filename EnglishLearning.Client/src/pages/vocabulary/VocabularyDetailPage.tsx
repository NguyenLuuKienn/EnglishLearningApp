import { useState, useEffect } from 'react'
import { useParams, Link } from 'react-router-dom'
import { vocabularyService } from '@/services/vocabularyService'
import { Vocabulary } from '@/types'
import { ArrowLeft, BookOpen } from 'lucide-react'

export default function VocabularyDetailPage() {
  const { id } = useParams<{ id: string }>()
  const [vocabulary, setVocabulary] = useState<Vocabulary | null>(null)
  const [isLoading, setIsLoading] = useState(true)

  useEffect(() => {
    if (!id) return
    setIsLoading(true)
    vocabularyService
      .getById(id)
      .then(setVocabulary)
      .catch(console.error)
      .finally(() => setIsLoading(false))
  }, [id])

  if (isLoading) {
    return (
      <div className="flex justify-center py-12">
        <div className="h-8 w-8 animate-spin rounded-full border-4 border-primary-600 border-t-transparent" />
      </div>
    )
  }

  if (!vocabulary) {
    return (
      <div className="card text-center">
        <p className="text-gray-600">Vocabulary not found</p>
        <Link to="/vocabulary" className="mt-4 inline-block btn btn-secondary">
          <ArrowLeft className="mr-2 h-4 w-4" />
          Back to Vocabulary
        </Link>
      </div>
    )
  }

  const difficultyColor: Record<string, string> = {
    Beginner: 'badge-success',
    Intermediate: 'badge-warning',
    Advanced: 'badge-danger',
  }

  return (
    <div>
      <Link to="/vocabulary" className="mb-4 inline-flex items-center gap-2 text-sm text-gray-600 hover:text-gray-900">
        <ArrowLeft className="h-4 w-4" />
        Back to Vocabulary
      </Link>

      <div className="card max-w-2xl">
        <div className="flex items-start justify-between">
          <div className="flex items-center gap-3">
            <div className="flex h-12 w-12 items-center justify-center rounded-lg bg-primary-100">
              <BookOpen className="h-6 w-6 text-primary-600" />
            </div>
            <div>
              <h1 className="text-2xl font-bold text-gray-900">{vocabulary.word}</h1>
              <span className={`mt-1 inline-block badge ${difficultyColor[vocabulary.difficulty]}`}>
                {vocabulary.difficulty}
              </span>
            </div>
          </div>
        </div>

        <div className="mt-6">
          <h2 className="text-sm font-medium text-gray-500">Definition</h2>
          <p className="mt-2 text-lg text-gray-900">{vocabulary.definition}</p>
        </div>

        {vocabulary.example && (
          <div className="mt-6">
            <h2 className="text-sm font-medium text-gray-500">Example</h2>
            <p className="mt-2 rounded-lg bg-gray-50 p-4 text-gray-700 italic">
              &quot;{vocabulary.example}&quot;
            </p>
          </div>
        )}
      </div>
    </div>
  )
}
