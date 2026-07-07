import api from './api'
import { Vocabulary, DifficultyLevel, ApiResponse } from '@/types'

export const vocabularyService = {
  getAll: async (
    page: number = 1,
    pageSize: number = 20,
    difficulty?: DifficultyLevel,
    search?: string,
  ): Promise<{ items: Vocabulary[]; totalRecords: number }> => {
    const response = await api.get<ApiResponse<{ items: Vocabulary[]; totalRecords: number }>>('/vocabularies', {
      params: { pageNumber: page, pageSize, difficulty, search },
    })
    return response.data.data!
  },

  getById: async (id: string): Promise<Vocabulary> => {
    const response = await api.get<ApiResponse<Vocabulary>>(`/vocabularies/${id}`)
    return response.data.data!
  },

  create: async (data: Partial<Vocabulary>): Promise<Vocabulary> => {
    const response = await api.post<ApiResponse<Vocabulary>>('/vocabularies', data)
    return response.data.data!
  },

  update: async (id: string, data: Partial<Vocabulary>): Promise<Vocabulary> => {
    const response = await api.put<ApiResponse<Vocabulary>>(`/vocabularies/${id}`, data)
    return response.data.data!
  },

  delete: async (id: string): Promise<void> => {
    await api.delete(`/vocabularies/${id}`)
  },
}
