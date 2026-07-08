import { DifficultyLevel } from './vocabulary'
import { UserRole } from './auth'

export interface Quiz {
  id: string
  title: string
  description?: string
  difficulty: DifficultyLevel
  timeLimitMinutes?: number
  passingScore?: number
  createdAt: string
}

export interface Question {
  id: string
  questionText: string
  questionType: QuestionType
  choices: ChoiceForTake[]
}

export interface ChoiceForTake {
  id: string
  choiceText: string
}

export interface Choice extends ChoiceForTake {
  isCorrect: boolean
}

export type QuestionType = 'MultipleChoice' | 'TrueFalse' | 'FillInBlank'

export interface QuizAssignment {
  id: string
  quizId: string
  quizTitle: string
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
  selectedChoiceId?: string
  answerText?: string
}
