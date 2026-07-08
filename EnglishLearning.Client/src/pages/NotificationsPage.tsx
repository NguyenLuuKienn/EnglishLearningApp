import { useState, useEffect } from 'react'
import { useAuth } from '@/store/AuthContext'
import { notificationService } from '@/services/notificationService'
import { Notification, NotificationType } from '@/types'
import { Bell } from 'lucide-react'

export default function NotificationsPage() {
  const { user } = useAuth()
  const [notifications, setNotifications] = useState<Notification[]>([])
  const [isLoading, setIsLoading] = useState(true)

  useEffect(() => {
    if (!user) return
    notificationService
      .getUserNotifications(user.id)
      .then((data) => setNotifications(data.items || []))
      .catch((error) => {
        console.error('Failed to load notifications:', error)
        setNotifications([])
      })
      .finally(() => setIsLoading(false))
  }, [user])

  const handleMarkRead = async (id: string) => {
    try {
      await notificationService.markAsRead(id)
      setNotifications((prev) => prev.map((n) => (n.id === id ? { ...n, isRead: true } : n)))
    } catch {
      console.error('Failed to mark as read')
    }
  }

  const typeIcons: Record<NotificationType, string> = {
    QuizAssigned: '📝',
    QuizStartingSoon: '⏰',
    QuizStarted: '🚀',
    QuizEnded: '🏁',
    QuizResultAvailable: '📊',
  }

  return (
    <div>
      <div className="mb-6">
        <h1 className="text-2xl font-bold text-gray-900">Notifications</h1>
        <p className="text-gray-600">Stay updated with your learning activities</p>
      </div>

      {isLoading ? (
        <div className="flex justify-center py-12">
          <div className="h-8 w-8 animate-spin rounded-full border-4 border-primary-600 border-t-transparent" />
        </div>
      ) : notifications.length === 0 ? (
        <div className="card text-center">
          <Bell className="mx-auto h-12 w-12 text-gray-300" />
          <p className="mt-4 text-gray-600">No notifications yet</p>
        </div>
      ) : (
        <div className="space-y-3">
          {notifications.map((n) => (
            <div
              key={n.id}
              className={`card cursor-pointer transition-colors ${
                !n.isRead ? 'border-l-4 border-l-primary-500 bg-primary-50/30' : ''
              }`}
              onClick={() => !n.isRead && handleMarkRead(n.id)}
            >
              <div className="flex items-start gap-4">
                <span className="text-2xl">{typeIcons[n.type]}</span>
                <div className="flex-1">
                  <h3 className="font-medium text-gray-900">{n.title}</h3>
                  <p className="mt-1 text-sm text-gray-600">{n.message}</p>
                  <p className="mt-1 text-xs text-gray-400">
                    {new Date(n.createdAt).toLocaleString()}
                  </p>
                </div>
                {!n.isRead && (
                  <div className="flex h-2 w-2 items-center justify-center">
                    <div className="h-2 w-2 rounded-full bg-primary-500" />
                  </div>
                )}
              </div>
            </div>
          ))}
        </div>
      )}
    </div>
  )
}
