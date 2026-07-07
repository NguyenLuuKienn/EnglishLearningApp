import { DifficultyLevel } from './vocabulary'
import { UserRole } from './auth'

export interface Quiz {
  id: string
  title: string
  description?: string
  difficulty: DifficultyLevel
  timeLimitMinutes?: number
  createdAt: string
}

export interface Question {
  id: string
  quizId: string
  text: string
  questionType: QuestionType
  choices: Choice[]
  correctAnswerIndex: number
  order: number
}

export interface Choice {
  id: string
  questionId: string
  text: string
  isCorrect: boolean
  order: number
}

export type QuestionType = 'MultipleChoice' | 'TrueFalse' | 'FillInBlank'

export interface QuizAssignment {
  id: string
  quizId: string
  quiz: Quiz
  targetRole?: UserRole
  targetUserId?: string
  startTime: string
  endTime: string
  status: AssignmentStatus
}

export type AssignmentStatus = 'Scheduled' | 'Active' | 'Completed' | 'Cancelled'

export interface QuizResult {
  id: string
  quizId: string
  userId: string
  score: number
  totalQuestions: number
  correctAnswers: number
  submittedAt: string
  answers: QuizAnswer[]
}

export interface QuizAnswer {
  questionId: string
  selectedAnswerIndex: number
  isCorrect: boolean
}
