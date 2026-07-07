import api from './api'
import { Notification, ApiResponse } from '@/types'

export const notificationService = {
  getUserNotifications: async (
    userId: string,
    page: number = 1,
    pageSize: number = 20,
    isRead?: boolean,
  ): Promise<{ items: Notification[]; totalRecords: number }> => {
    const response = await api.get<ApiResponse<{ items: Notification[]; totalRecords: number }>>(
      `/notifications/user/${userId}`,
      { params: { pageNumber: page, pageSize, isRead } },
    )
    return response.data.data!
  },

  markAsRead: async (id: string): Promise<void> => {
    await api.patch(`/notifications/${id}/read`)
  },

  getUnreadCount: async (userId: string): Promise<number> => {
    const response = await api.get<ApiResponse<number>>(`/notifications/user/${userId}/unread-count`)
    return response.data.data!
  },
}
