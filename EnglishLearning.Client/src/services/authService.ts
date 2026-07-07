import api from './api'
import { LoginRequest, RegisterRequest, TokenResponse, User, ApiResponse } from '@/types'

export const authService = {
  login: async (data: LoginRequest): Promise<TokenResponse> => {
    const response = await api.post<ApiResponse<TokenResponse>>('/auth/login', data)
    return response.data.data!
  },

  register: async (data: RegisterRequest): Promise<string> => {
    const response = await api.post<ApiResponse<string>>('/auth/register', data)
    return response.data.data!
  },

  refreshToken: async (accessToken: string, refreshToken: string): Promise<TokenResponse> => {
    const response = await api.post<ApiResponse<TokenResponse>>('/auth/refresh-token', {
      accessToken,
      refreshToken,
    })
    return response.data.data!
  },

  getProfile: async (): Promise<User> => {
    const response = await api.get<ApiResponse<User>>('/auth/profile')
    return response.data.data!
  },
}
