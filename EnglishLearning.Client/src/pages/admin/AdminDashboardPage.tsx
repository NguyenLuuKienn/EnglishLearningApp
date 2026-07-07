import { Link } from 'react-router-dom'
import { BookOpen, Users, ClipboardList } from 'lucide-react'

export default function AdminDashboardPage() {
  const menuItems = [
    { path: '/admin/quizzes', label: 'Manage Quizzes', icon: BookOpen, desc: 'Create and edit quizzes' },
    { path: '/admin/quizzes/assign', label: 'Assign Quizzes', icon: ClipboardList, desc: 'Assign quizzes to students' },
    { path: '/admin/vocabulary', label: 'Manage Vocabulary', icon: Users, desc: 'Add and edit vocabulary words' },
  ]

  return (
    <div>
      <div className="mb-6">
        <h1 className="text-2xl font-bold text-gray-900">Admin Dashboard</h1>
        <p className="text-gray-600">Manage content and assignments</p>
      </div>

      <div className="grid gap-4 sm:grid-cols-2 lg:grid-cols-3">
        {menuItems.map(({ path, label, icon: Icon, desc }) => (
          <Link key={path} to={path} className="card hover:shadow-md transition-shadow">
            <div className="flex items-center gap-4">
              <div className="flex h-12 w-12 items-center justify-center rounded-lg bg-primary-100">
                <Icon className="h-6 w-6 text-primary-600" />
              </div>
              <div>
                <h3 className="font-semibold text-gray-900">{label}</h3>
                <p className="text-sm text-gray-500">{desc}</p>
              </div>
            </div>
          </Link>
        ))}
      </div>
    </div>
  )
}
