# Task 8.10: Layout Components

## Description

Create layout components: Navbar, Sidebar, MainLayout.

## Priority
🔴 Critical — App structure

## Dependencies
- Task 8.9 (UI Components), Task 8.7 (Auth Context)

## Files to Create

| File | Action |
|------|--------|
| `EnglishLearning.Web/src/components/layout/Navbar.tsx` | Create |
| `EnglishLearning.Web/src/components/layout/Sidebar.tsx` | Create |
| `EnglishLearning.Web/src/components/layout/MainLayout.tsx` | Create |

## Steps

### Step 1: Create Navbar
1. Logo, navigation links, user menu (avatar, logout)

### Step 2: Create Sidebar
1. Navigation menu with icons
2. Active route highlighting

### Step 3: Create MainLayout
1. Combines Navbar + Sidebar + Content area

## Expected Code

```typescript
// Navbar.tsx
import { Link } from 'react-router-dom';
import useAuth from '../../hooks/useAuth';
import { LogOut, User } from 'lucide-react';

const Navbar: React.FC = () => {
  const { user, logout } = useAuth();

  return (
    <nav className="bg-white border-b border-gray-200 px-6 py-4 flex items-center justify-between">
      <Link to="/dashboard" className="text-xl font-bold text-blue-600">
        English Learning
      </Link>
      <div className="flex items-center gap-4">
        {user && (
          <div className="flex items-center gap-3">
            <span className="text-sm text-gray-600">{user.username}</span>
            <Link to="/profile" className="p-2 rounded-full hover:bg-gray-100">
              <User className="w-5 h-5" />
            </Link>
            <button onClick={logout} className="p-2 rounded-full hover:bg-gray-100">
              <LogOut className="w-5 h-5" />
            </button>
          </div>
        )}
      </div>
    </nav>
  );
};

export default Navbar;

// Sidebar.tsx
import { Link, useLocation } from 'react-router-dom';
import { BookOpen, ClipboardList, BarChart3, History, Trophy } from 'lucide-react';

const navItems = [
  { path: '/dashboard', label: 'Dashboard', icon: BarChart3 },
  { path: '/vocabulary', label: 'Vocabulary', icon: BookOpen },
  { path: '/quiz', label: 'Quizzes', icon: ClipboardList },
  { path: '/history', label: 'History', icon: History },
  { path: '/leaderboard', label: 'Leaderboard', icon: Trophy },
];

const Sidebar: React.FC = () => {
  const location = useLocation();

  return (
    <aside className="w-64 bg-white border-r border-gray-200 min-h-screen p-4">
      <nav className="space-y-2">
        {navItems.map(({ path, label, icon: Icon }) => {
          const isActive = location.pathname === path;
          return (
            <Link
              key={path}
              to={path}
              className={`flex items-center gap-3 px-4 py-3 rounded-lg transition-colors ${
                isActive ? 'bg-blue-50 text-blue-600' : 'text-gray-600 hover:bg-gray-50'
              }`}
            >
              <Icon className="w-5 h-5" />
              <span className="font-medium">{label}</span>
            </Link>
          );
        })}
      </nav>
    </aside>
  );
};

export default Sidebar;

// MainLayout.tsx
import Navbar from './Navbar';
import Sidebar from './Sidebar';

interface MainLayoutProps {
  children: React.ReactNode;
}

const MainLayout: React.FC<MainLayoutProps> = ({ children }) => {
  return (
    <div className="min-h-screen bg-gray-50">
      <Navbar />
      <div className="flex">
        <Sidebar />
        <main className="flex-1 p-6">{children}</main>
      </div>
    </div>
  );
};

export default MainLayout;
```

## Verification

- [ ] All layout components created
- [ ] Navigation works correctly
- [ ] Active route highlighted

## Acceptance Criteria

- [ ] `Navbar` with logo, user info, logout button
- [ ] `Sidebar` with navigation menu and icons
- [ ] `MainLayout` combines Navbar + Sidebar + Content
- [ ] Active route highlighting in sidebar
- [ ] Responsive design
