import api from './api'
import {
  Quiz,
  QuizAssignment,
  QuizResult,
  QuizAnswer,
  UserRole,
  ApiResponse,
} from '@/types'

interface PagedData<T> {
  pageNumber: number
  pageSize: number
  totalRecords: number
  totalPages: number
  success: boolean
  message: string
  data: T[]
}

export const quizService = {
  // Quizzes
  getAll: async (): Promise<Quiz[]> => {
    const response = await api.get<PagedData<Quiz>>('/quizzes')
    return response.data.data || []
  },

  getById: async (id: string): Promise<Quiz> => {
    const response = await api.get<ApiResponse<Quiz>>(`/quizzes/${id}`)
    return response.data.data!
  },

  getForTake: async (id: string): Promise<Quiz> => {
    const response = await api.get<ApiResponse<Quiz>>(`/quizzes/${id}/take`)
    return response.data.data!
  },

  create: async (data: {
    title: string
    description?: string
    difficulty: string
    timeLimitMinutes: number
  }): Promise<string> => {
    const response = await api.post<ApiResponse<string>>('/quizzes', {
      ...data,
      passingScore: 50,
      questions: [],
    })
    return response.data.data!
  },

  update: async (id: string, data: {
    title: string
    description?: string
    difficulty: string
    timeLimitMinutes: number
  }): Promise<string> => {
    const response = await api.put<ApiResponse<string>>(`/quizzes/${id}`, data)
    return response.data.data!
  },

  delete: async (id: string): Promise<void> => {
    await api.delete(`/quizzes/${id}`)
  },

  // Assignments
  getUserAssignmentsByUserId: async (userId: string): Promise<QuizAssignment[]> => {
    const response = await api.get<ApiResponse<QuizAssignment[]>>(`/assignments/user/${userId}`)
    return response.data.data!
  },

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

  // Questions
  getQuestionsByQuizId: async (quizId: string): Promise<any[]> => {
    const quiz = await quizService.getById(quizId)
    return quiz.questions || []
  },

  addQuestion: async (quizId: string, question: {
    questionText: string
    questionType: string
    difficulty: string
    correctAnswer?: string
    choices: { choiceText: string; isCorrect: boolean }[]
  }): Promise<string> => {
    const response = await api.post<ApiResponse<string>>(`/quizzes/${quizId}/questions`, question)
    return response.data.data!
  },

  updateQuestion: async (quizId: string, questionId: string, question: {
    questionText: string
    questionType: string
    difficulty: string
    correctAnswer?: string
    choices: { choiceText: string; isCorrect: boolean }[]
  }): Promise<string> => {
    const response = await api.put<ApiResponse<string>>(`/quizzes/${quizId}/questions/${questionId}`, question)
    return response.data.data!
  },

  deleteQuestion: async (quizId: string, questionId: string): Promise<void> => {
    await api.delete(`/quizzes/${quizId}/questions/${questionId}`)
  },

  // Quiz Results
  submitResult: async (data: {
    quizId: string
    userId: string
    answers: QuizAnswer[]
  }): Promise<QuizResult> => {
    const response = await api.post<ApiResponse<QuizResult>>('/quizresults/submit', data)
    return response.data.data!
  },

  getResult: async (id: string): Promise<QuizResult> => {
    const response = await api.get<ApiResponse<QuizResult>>(`/quizresults/${id}`)
    return response.data.data!
  },
}
