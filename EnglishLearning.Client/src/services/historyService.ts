import api from './api'
import { LearningHistory, Leaderboard, ApiResponse } from '@/types'

interface PagedData<T> {
  pageNumber: number
  pageSize: number
  totalRecords: number
  totalPages: number
  success: boolean
  message: string
  data: T[]
}

export const historyService = {
  getUserHistory: async (
    userId: string,
    page: number = 1,
    pageSize: number = 10,
  ): Promise<{ items: LearningHistory[]; totalRecords: number }> => {
    const response = await api.get<PagedData<LearningHistory>>(
      `/history/user/${userId}`,
      { params: { pageNumber: page, pageSize } },
    )
    return { items: response.data.data || [], totalRecords: response.data.totalRecords }
  },

  recordHistory: async (data: {
    actionType: string
    targetId?: string
    description: string
  }): Promise<void> => {
    await api.post('/history', data)
  },
}

export const leaderboardService = {
  getLeaderboard: async (count: number = 20): Promise<Leaderboard[]> => {
    const response = await api.get<ApiResponse<Leaderboard[]>>('/leaderboard', {
      params: { count },
    })
    return response.data.data!
  },

  getUserRank: async (userId: string): Promise<number> => {
    const response = await api.get<ApiResponse<number>>(`/leaderboard/user/${userId}/rank`)
    return response.data.data!
  },
}
