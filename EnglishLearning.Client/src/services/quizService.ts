import api from './api'
import {
  Quiz,
  QuizAssignment,
  QuizResult,
  QuizAnswer,
  UserRole,
  ApiResponse,
} from '@/types'

export const quizService = {
  // Quizzes
  getAll: async (): Promise<Quiz[]> => {
    const response = await api.get<ApiResponse<Quiz[]>>('/quizzes')
    return response.data.data!
  },

  getById: async (id: string): Promise<Quiz> => {
    const response = await api.get<ApiResponse<Quiz>>(`/quizzes/${id}`)
    return response.data.data!
  },

  create: async (data: Partial<Quiz>): Promise<Quiz> => {
    const response = await api.post<ApiResponse<Quiz>>('/quizzes', data)
    return response.data.data!
  },

  update: async (id: string, data: Partial<Quiz>): Promise<Quiz> => {
    const response = await api.put<ApiResponse<Quiz>>(`/quizzes/${id}`, data)
    return response.data.data!
  },

  delete: async (id: string): Promise<void> => {
    await api.delete(`/quizzes/${id}`)
  },

  // Assignments
  getUserAssignments: async (): Promise<QuizAssignment[]> => {
    const response = await api.get<ApiResponse<QuizAssignment[]>>('/assignments/user')
    return response.data.data!
  },

  getActiveAssignments: async (): Promise<QuizAssignment[]> => {
    const response = await api.get<ApiResponse<QuizAssignment[]>>('/assignments/active')
    return response.data.data!
  },

  assignQuiz: async (data: {
    quizId: string
    targetRole?: UserRole
    targetUserId?: string
    startTime: string
    endTime: string
  }): Promise<QuizAssignment> => {
    const response = await api.post<ApiResponse<QuizAssignment>>('/assignments', data)
    return response.data.data!
  },

  cancelAssignment: async (id: string): Promise<void> => {
    await api.patch(`/assignments/${id}/cancel`)
  },

  // Quiz Results
  submitResult: async (data: {
    quizId: string
    answers: QuizAnswer[]
  }): Promise<QuizResult> => {
    const response = await api.post<ApiResponse<QuizResult>>('/quiz-results', data)
    return response.data.data!
  },

  getResult: async (id: string): Promise<QuizResult> => {
    const response = await api.get<ApiResponse<QuizResult>>(`/quiz-results/${id}`)
    return response.data.data!
  },
}
