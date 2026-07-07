export interface User {
  id: string
  username: string
  email: string
  role: UserRole
  avatarUrl?: string
  createdAt: string
}

export type UserRole = 'Admin' | 'Teacher' | 'Student'

export interface TokenResponse {
  accessToken: string
  refreshToken: string
  expiresIn: number
}

// API response wrapper from BE
export interface ApiResponse<T> {
  success: boolean
  message: string
  data?: T
  errors?: string[]
}

export interface LoginRequest {
  username: string
  password: string
}

export interface RegisterRequest {
  username: string
  email: string
  password: string
}
