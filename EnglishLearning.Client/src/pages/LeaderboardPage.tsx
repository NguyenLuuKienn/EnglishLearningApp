import { useState, useEffect } from 'react'
import { leaderboardService } from '@/services/historyService'
import { Leaderboard } from '@/types'
import { Trophy, Medal, Award } from 'lucide-react'

export default function LeaderboardPage() {
  const [leaderboard, setLeaderboard] = useState<Leaderboard[]>([])
  const [isLoading, setIsLoading] = useState(true)

  useEffect(() => {
    leaderboardService
      .getLeaderboard()
      .then((data) => setLeaderboard(data || []))
      .catch((error) => {
        console.error('Failed to load leaderboard:', error)
        setLeaderboard([])
      })
      .finally(() => setIsLoading(false))
  }, [])

  const getRankIcon = (rank: number) => {
    switch (rank) {
      case 1:
        return <Trophy className="h-6 w-6 text-yellow-500" />
      case 2:
        return <Medal className="h-6 w-6 text-gray-400" />
      case 3:
        return <Award className="h-6 w-6 text-amber-600" />
      default:
        return <span className="text-lg font-bold text-gray-400">#{rank}</span>
    }
  }

  return (
    <div>
      <div className="mb-6">
        <h1 className="text-2xl font-bold text-gray-900">Leaderboard</h1>
        <p className="text-gray-600">Top learners ranked by performance</p>
      </div>

      {isLoading ? (
        <div className="flex justify-center py-12">
          <div className="h-8 w-8 animate-spin rounded-full border-4 border-primary-600 border-t-transparent" />
        </div>
      ) : leaderboard.length === 0 ? (
        <div className="card text-center">
          <Trophy className="mx-auto h-12 w-12 text-gray-300" />
          <p className="mt-4 text-gray-600">No leaderboard data yet</p>
        </div>
      ) : (
        <div className="card">
          <div className="overflow-x-auto">
            <table className="w-full">
              <thead>
                <tr className="border-b border-gray-200">
                  <th className="py-3 px-4 text-left text-sm font-medium text-gray-500">Rank</th>
                  <th className="py-3 px-4 text-left text-sm font-medium text-gray-500">User</th>
                  <th className="py-3 px-4 text-center text-sm font-medium text-gray-500">Score</th>
                  <th className="py-3 px-4 text-center text-sm font-medium text-gray-500">Quizzes</th>
                  <th className="py-3 px-4 text-center text-sm font-medium text-gray-500">Avg</th>
                  <th className="py-3 px-4 text-center text-sm font-medium text-gray-500">Streak</th>
                </tr>
              </thead>
              <tbody>
                {leaderboard.map((entry) => (
                  <tr key={entry.id} className="border-b border-gray-100 hover:bg-gray-50">
                    <td className="py-4 px-4">{getRankIcon(entry.rank)}</td>
                    <td className="py-4 px-4">
                      <div className="flex items-center gap-3">
                        <div className="flex h-8 w-8 items-center justify-center rounded-full bg-primary-100 text-sm font-medium text-primary-700">
                          {entry.username.charAt(0).toUpperCase()}
                        </div>
                        <span className="font-medium text-gray-900">{entry.username}</span>
                      </div>
                    </td>
                    <td className="py-4 px-4 text-center font-semibold text-gray-900">
                      {entry.totalScore}
                    </td>
                    <td className="py-4 px-4 text-center text-gray-600">
                      {entry.quizzesCompleted}
                    </td>
                    <td className="py-4 px-4 text-center text-gray-600">
                      {entry.averageScore.toFixed(1)}%
                    </td>
                    <td className="py-4 px-4 text-center text-gray-600">
                      {entry.streak} 🔥
                    </td>
                  </tr>
                ))}
              </tbody>
            </table>
          </div>
        </div>
      )}
    </div>
  )
}
