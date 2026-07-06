# Task 8.12: Dashboard Page

## Description

Create Dashboard page with stats overview, recent activity, and quick actions.

## Priority
🔴 Critical — Main landing page after login

## Dependencies
- Task 8.10 (Layout), Task 8.6 (API Services)

## Files to Create

| File | Action |
|------|--------|
| `EnglishLearning.Web/src/pages/DashboardPage.tsx` | Create |

## Steps

### Step 1: Create DashboardPage
1. Stats cards: Quizzes Completed, Average Score, Current Streak, Rank
2. Recent activity (from history)
3. Quick action buttons (Start Quiz, Browse Vocabulary)

## Expected Code

```typescript
import { useQuery } from '@tanstack/react-query';
import { Link } from 'react-router-dom';
import useAuth from '../hooks/useAuth';
import { historyService } from '../services/history.service';
import { leaderboardService } from '../services/leaderboard.service';
import MainLayout from '../components/layout/MainLayout';
import Card from '../components/ui/Card';
import Button from '../components/ui/Button';
import Badge from '../components/ui/Badge';
import { BookOpen, Trophy, Flame, Target } from 'lucide-react';

const DashboardPage: React.FC = () => {
  const { user } = useAuth();

  const { data: rankData } = useQuery({
    queryKey: ['userRank', user?.id],
    queryFn: () => leaderboardService.getUserRank(user!.id),
    enabled: !!user,
  });

  const { data: historyData } = useQuery({
    queryKey: ['recentHistory', user?.id],
    queryFn: () => historyService.getUserHistory(user!.id, 1, 5),
    enabled: !!user,
  });

  const recentActivities = historyData?.data || [];
  const rank = rankData?.data || '-';

  return (
    <MainLayout>
      <div className="space-y-6">
        <h1 className="text-2xl font-bold text-gray-900">Welcome back, {user?.username}! 👋</h1>

        {/* Stats */}
        <div className="grid grid-cols-1 md:grid-cols-4 gap-4">
          <Card className="flex items-center gap-4">
            <div className="p-3 bg-blue-100 rounded-lg"><BookOpen className="w-6 h-6 text-blue-600" /></div>
            <div><p className="text-sm text-gray-600">Quizzes Done</p><p className="text-2xl font-bold">0</p></div>
          </Card>
          <Card className="flex items-center gap-4">
            <div className="p-3 bg-green-100 rounded-lg"><Target className="w-6 h-6 text-green-600" /></div>
            <div><p className="text-sm text-gray-600">Avg Score</p><p className="text-2xl font-bold">0%</p></div>
          </Card>
          <Card className="flex items-center gap-4">
            <div className="p-3 bg-orange-100 rounded-lg"><Flame className="w-6 h-6 text-orange-600" /></div>
            <div><p className="text-sm text-gray-600">Streak</p><p className="text-2xl font-bold">0 days</p></div>
          </Card>
          <Card className="flex items-center gap-4">
            <div className="p-3 bg-purple-100 rounded-lg"><Trophy className="w-6 h-6 text-purple-600" /></div>
            <div><p className="text-sm text-gray-600">Rank</p><p className="text-2xl font-bold">#{rank}</p></div>
          </Card>
        </div>

        {/* Quick Actions */}
        <div className="flex gap-4">
          <Link to="/quiz"><Button>Start Quiz</Button></Link>
          <Link to="/vocabulary"><Button variant="secondary">Browse Vocabulary</Button></Link>
        </div>

        {/* Recent Activity */}
        <Card>
          <h2 className="text-lg font-semibold mb-4">Recent Activity</h2>
          {recentActivities.length === 0 ? (
            <p className="text-gray-500">No activity yet. Start a quiz to begin!</p>
          ) : (
            <div className="space-y-3">
              {recentActivities.map((item: any) => (
                <div key={item.id} className="flex items-center justify-between py-2 border-b last:border-0">
                  <div>
                    <p className="font-medium">{item.actionType}</p>
                    <p className="text-sm text-gray-500">{new Date(item.createdAt).toLocaleDateString()}</p>
                  </div>
                  {item.score && <Badge variant="success">{item.score}%</Badge>}
                </div>
              ))}
            </div>
          )}
        </Card>
      </div>
    </MainLayout>
  );
};

export default DashboardPage;
```

## Verification

- [ ] Dashboard renders with stats
- [ ] Recent activity displayed
- [ ] Quick action buttons work

## Acceptance Criteria

- [ ] Stats cards: Quizzes Done, Avg Score, Streak, Rank
- [ ] Quick action buttons (Start Quiz, Browse Vocabulary)
- [ ] Recent activity list from history API
- [ ] Uses MainLayout wrapper
