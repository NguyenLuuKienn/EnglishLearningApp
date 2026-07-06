# Task 8.15: History & Leaderboard Pages

## Description

Create History page (learning activity timeline) and Leaderboard page (ranking table).

## Priority
🟡 High — Advanced features

## Dependencies
- Task 8.10 (Layout), Task 8.6 (API Services)

## Files to Create

| File | Action |
|------|--------|
| `EnglishLearning.Web/src/pages/HistoryPage.tsx` | Create |
| `EnglishLearning.Web/src/pages/LeaderboardPage.tsx` | Create |
| `EnglishLearning.Web/src/pages/ProfilePage.tsx` | Create |

## Expected Code

```typescript
// HistoryPage.tsx
import { useQuery } from '@tanstack/react-query';
import useAuth from '../hooks/useAuth';
import { historyService } from '../services/history.service';
import MainLayout from '../components/layout/MainLayout';
import Card from '../components/ui/Card';
import Badge from '../components/ui/Badge';
import { BookOpen, ClipboardList, Star } from 'lucide-react';

const actionTypeMap: Record<number, { label: string; icon: any; variant: 'success' | 'warning' | 'info' }> = {
  0: { label: 'View Vocabulary', icon: BookOpen, variant: 'info' },
  1: { label: 'Complete Quiz', icon: ClipboardList, variant: 'success' },
  2: { label: 'Bookmark Word', icon: Star, variant: 'warning' },
  3: { label: 'Start Quiz', icon: ClipboardList, variant: 'info' },
};

const HistoryPage: React.FC = () => {
  const { user } = useAuth();

  const { data, isLoading } = useQuery({
    queryKey: ['history', user?.id],
    queryFn: () => historyService.getUserHistory(user!.id, 1, 20),
    enabled: !!user,
  });

  const activities = data?.data || [];

  return (
    <MainLayout>
      <div className="space-y-6">
        <h1 className="text-2xl font-bold">Learning History</h1>
        {isLoading ? (
          <div className="flex justify-center py-12"><div className="animate-spin rounded-full h-12 w-12 border-b-2 border-blue-600" /></div>
        ) : activities.length === 0 ? (
          <Card className="text-center py-12"><p className="text-gray-500">No activity yet. Start learning!</p></Card>
        ) : (
          <div className="space-y-3">
            {activities.map((item: any) => {
              const action = actionTypeMap[item.actionType] || { label: 'Unknown', icon: BookOpen, variant: 'info' };
              const Icon = action.icon;
              return (
                <Card key={item.id} className="flex items-center gap-4">
                  <div className="p-2 bg-blue-100 rounded-lg"><Icon className="w-5 h-5 text-blue-600" /></div>
                  <div className="flex-1">
                    <p className="font-medium">{action.label}</p>
                    <p className="text-sm text-gray-500">{new Date(item.createdAt).toLocaleString()}</p>
                  </div>
                  {item.score && <Badge variant="success">{item.score}%</Badge>}
                </Card>
              );
            })}
          </div>
        )}
      </div>
    </MainLayout>
  );
};

export default HistoryPage;

// LeaderboardPage.tsx
import { useQuery } from '@tanstack/react-query';
import { leaderboardService } from '../services/leaderboard.service';
import MainLayout from '../components/layout/MainLayout';
import Card from '../components/ui/Card';
import Badge from '../components/ui/Badge';
import { Trophy, Medal, Award } from 'lucide-react';

const LeaderboardPage: React.FC = () => {
  const { data, isLoading } = useQuery({
    queryKey: ['leaderboard'],
    queryFn: () => leaderboardService.getLeaderboard(100),
  });

  const leaders = data?.data || [];

  const getRankIcon = (rank: number) => {
    if (rank === 1) return <Trophy className="w-6 h-6 text-yellow-500" />;
    if (rank === 2) return <Medal className="w-6 h-6 text-gray-400" />;
    if (rank === 3) return <Award className="w-6 h-6 text-orange-500" />;
    return <span className="text-lg font-bold text-gray-400 w-6 text-center">{rank}</span>;
  };

  return (
    <MainLayout>
      <div className="space-y-6">
        <h1 className="text-2xl font-bold">Leaderboard 🏆</h1>
        {isLoading ? (
          <div className="flex justify-center py-12"><div className="animate-spin rounded-full h-12 w-12 border-b-2 border-blue-600" /></div>
        ) : (
          <Card>
            <div className="space-y-2">
              {leaders.map((item: any) => (
                <div key={item.id} className="flex items-center gap-4 p-4 rounded-lg hover:bg-gray-50">
                  {getRankIcon(item.rank)}
                  <div className="flex-1">
                    <p className="font-medium">{item.username}</p>
                    <p className="text-sm text-gray-500">{item.quizzesCompleted} quizzes • {item.streak} day streak</p>
                  </div>
                  <div className="text-right">
                    <p className="font-bold text-lg">{item.totalScore}</p>
                    <p className="text-sm text-gray-500">Avg: {item.averageScore}%</p>
                  </div>
                </div>
              ))}
            </div>
          </Card>
        )}
      </div>
    </MainLayout>
  );
};

export default LeaderboardPage;

// ProfilePage.tsx
import useAuth from '../hooks/useAuth';
import MainLayout from '../components/layout/MainLayout';
import Card from '../components/ui/Card';
import Badge from '../components/ui/Badge';
import { User, Mail, Calendar } from 'lucide-react';

const ProfilePage: React.FC = () => {
  const { user } = useAuth();

  if (!user) return null;

  return (
    <MainLayout>
      <div className="max-w-2xl mx-auto space-y-6">
        <h1 className="text-2xl font-bold">Profile</h1>
        <Card className="text-center">
          <div className="w-24 h-24 bg-blue-100 rounded-full flex items-center justify-center mx-auto mb-4">
            <User className="w-12 h-12 text-blue-600" />
          </div>
          <h2 className="text-2xl font-bold">{user.username}</h2>
          <Badge variant="info" className="mt-2">{user.role}</Badge>
        </Card>
        <Card>
          <div className="space-y-4">
            <div className="flex items-center gap-3">
              <Mail className="w-5 h-5 text-gray-400" />
              <div><p className="text-sm text-gray-500">Email</p><p className="font-medium">{user.email}</p></div>
            </div>
            <div className="flex items-center gap-3">
              <Calendar className="w-5 h-5 text-gray-400" />
              <div><p className="text-sm text-gray-500">Member since</p><p className="font-medium">{new Date(user.createdAt).toLocaleDateString()}</p></div>
            </div>
          </div>
        </Card>
      </div>
    </MainLayout>
  );
};

export default ProfilePage;
```

## Verification

- [ ] History page shows activity timeline
- [ ] Leaderboard shows ranking
- [ ] Profile shows user info

## Acceptance Criteria

- [ ] `HistoryPage` with activity timeline
- [ ] `LeaderboardPage` with ranking table (top 3 icons)
- [ ] `ProfilePage` with user info
- [ ] Uses MainLayout wrapper
