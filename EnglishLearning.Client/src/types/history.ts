export interface LearningHistory {
  id: string
  userId: string
  actionType: ActionType
  targetId?: string
  description: string
  createdAt: string
}

export type ActionType = 'ViewVocabulary' | 'CompleteQuiz' | 'BookmarkWord' | 'StartQuiz'

export interface Leaderboard {
  id: string
  userId: string
  username: string
  avatarUrl?: string
  totalScore: number
  quizzesCompleted: number
  averageScore: number
  streak: number
  rank: number
}
