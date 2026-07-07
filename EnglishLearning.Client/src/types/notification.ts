export interface Notification {
  id: string
  userId: string
  type: NotificationType
  title: string
  message: string
  isRead: boolean
  data?: string
  createdAt: string
}

export type NotificationType =
  | 'QuizAssigned'
  | 'QuizStartingSoon'
  | 'QuizStarted'
  | 'QuizEnded'
  | 'QuizResultAvailable'
