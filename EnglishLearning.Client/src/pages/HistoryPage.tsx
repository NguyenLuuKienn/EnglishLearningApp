import { useState, useEffect } from 'react'
import { useAuth } from '@/store/AuthContext'
import { historyService } from '@/services/historyService'
import { LearningHistory } from '@/types'
import { History, Eye, BookOpen, Bookmark, PlayCircle } from 'lucide-react'

export default function HistoryPage() {
  const { user } = useAuth()
  const [history, setHistory] = useState<LearningHistory[]>([])
  const [isLoading, setIsLoading] = useState(true)

  useEffect(() => {
    if (!user) return
    historyService
      .getUserHistory(user.id)
      .then((data) => setHistory(data.items || []))
      .catch((error) => {
        console.error('Failed to load history:', error)
        setHistory([])
      })
      .finally(() => setIsLoading(false))
  }, [user])

  const actionIcons: Record<string, { icon: any; color: string }> = {
    ViewVocabulary: { icon: Eye, color: 'text-blue-500 bg-blue-100' },
    CompleteQuiz: { icon: BookOpen, color: 'text-green-500 bg-green-100' },
    BookmarkWord: { icon: Bookmark, color: 'text-yellow-500 bg-yellow-100' },
    StartQuiz: { icon: PlayCircle, color: 'text-purple-500 bg-purple-100' },
  }

  return (
    <div>
      <div className="mb-6">
        <h1 className="text-2xl font-bold text-gray-900">Learning History</h1>
        <p className="text-gray-600">Your recent learning activities</p>
      </div>

      {isLoading ? (
        <div className="flex justify-center py-12">
          <div className="h-8 w-8 animate-spin rounded-full border-4 border-primary-600 border-t-transparent" />
        </div>
      ) : history.length === 0 ? (
        <div className="card text-center">
          <History className="mx-auto h-12 w-12 text-gray-300" />
          <p className="mt-4 text-gray-600">No learning history yet</p>
        </div>
      ) : (
        <div className="space-y-3">
          {history.map((item) => {
            const config = (actionIcons as any)[item.actionType] || actionIcons.ViewVocabulary
            const Icon = config.icon

            return (
              <div key={item.id} className="card flex items-center gap-4">
                <div className={`flex h-10 w-10 items-center justify-center rounded-lg ${config.color}`}>
                  <Icon className="h-5 w-5" />
                </div>
                <div className="flex-1">
                  <p className="font-medium text-gray-900">{item.description}</p>
                  <p className="text-sm text-gray-500">
                    {new Date(item.createdAt).toLocaleString()}
                  </p>
                </div>
              </div>
            )
          })}
        </div>
      )}
    </div>
  )
}
