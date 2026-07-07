import { useState, useEffect } from 'react'
import { Link } from 'react-router-dom'
import { Search, Filter, ChevronLeft, ChevronRight } from 'lucide-react'
import { vocabularyService } from '@/services/vocabularyService'
import { Vocabulary, DifficultyLevel } from '@/types'

export default function VocabularyPage() {
  const [vocabularies, setVocabularies] = useState<Vocabulary[]>([])
  const [search, setSearch] = useState('')
  const [difficulty, setDifficulty] = useState<DifficultyLevel | ''>('')
  const [page, setPage] = useState(1)
  const [totalRecords, setTotalRecords] = useState(0)
  const [isLoading, setIsLoading] = useState(false)

  const pageSize = 12

  const loadVocabularies = async () => {
    setIsLoading(true)
    try {
      const data = await vocabularyService.getAll(
        page,
        pageSize,
        difficulty ? difficulty : undefined,
        search || undefined,
      )
      setVocabularies(data.items || [])
      setTotalRecords(data.totalRecords || 0)
    } catch {
      setVocabularies([])
      setTotalRecords(0)
    } finally {
      setIsLoading(false)
    }
  }

  useEffect(() => {
    loadVocabularies()
  }, [page, difficulty, search])

  const totalPages = Math.ceil(totalRecords / pageSize)

  const difficultyColor: Record<string, string> = {
    Beginner: 'badge-success',
    Intermediate: 'badge-warning',
    Advanced: 'badge-danger',
  }

  return (
    <div>
      <div className="mb-6 flex flex-col gap-4 sm:flex-row sm:items-center sm:justify-between">
        <div>
          <h1 className="text-2xl font-bold text-gray-900">Vocabulary</h1>
          <p className="text-gray-600">Browse and learn new words</p>
        </div>
      </div>

      {/* Filters */}
      <div className="mb-6 flex flex-col gap-3 sm:flex-row">
        <div className="relative flex-1">
          <Search className="absolute left-3 top-1/2 h-4 w-4 -translate-y-1/2 text-gray-400" />
          <input
            type="text"
            placeholder="Search words..."
            className="input pl-10"
            value={search}
            onChange={(e) => setSearch(e.target.value)}
            onKeyDown={(e) => e.key === 'Enter' && loadVocabularies()}
          />
        </div>
        <div className="flex items-center gap-2">
          <Filter className="h-4 w-4 text-gray-400" />
          <select
            className="input w-40"
            value={difficulty}
            onChange={(e) => setDifficulty(e.target.value as DifficultyLevel | '')}
          >
            <option value="">All Levels</option>
            <option value="Beginner">Beginner</option>
            <option value="Intermediate">Intermediate</option>
            <option value="Advanced">Advanced</option>
          </select>
          <button className="btn btn-primary" onClick={loadVocabularies}>
            Search
          </button>
        </div>
      </div>

      {/* Grid */}
      {isLoading ? (
        <div className="flex justify-center py-12">
          <div className="h-8 w-8 animate-spin rounded-full border-4 border-primary-600 border-t-transparent" />
        </div>
      ) : (
        <div className="grid gap-4 sm:grid-cols-2 lg:grid-cols-3">
          {vocabularies.map((v) => (
            <Link key={v.id} to={`/vocabulary/${v.id}`} className="card hover:shadow-md transition-shadow">
              <div className="flex items-start justify-between">
                <h3 className="text-lg font-semibold text-gray-900">{v.word}</h3>
                <span className={`badge ${difficultyColor[v.difficulty] || 'badge-primary'}`}>
                  {v.difficulty}
                </span>
              </div>
              <p className="mt-2 text-sm text-gray-600 line-clamp-2">{v.definition}</p>
              {v.example && (
                <p className="mt-2 text-sm italic text-gray-400">&quot;{v.example}&quot;</p>
              )}
            </Link>
          ))}
        </div>
      )}

      {/* Pagination */}
      {totalPages > 1 && (
        <div className="mt-6 flex items-center justify-center gap-2">
          <button
            className="btn btn-secondary"
            disabled={page <= 1}
            onClick={() => setPage((p) => p - 1)}
          >
            <ChevronLeft className="h-4 w-4" />
          </button>
          <span className="text-sm text-gray-600">
            Page {page} of {totalPages}
          </span>
          <button
            className="btn btn-secondary"
            disabled={page >= totalPages}
            onClick={() => setPage((p) => p + 1)}
          >
            <ChevronRight className="h-4 w-4" />
          </button>
        </div>
      )}
    </div>
  )
}
