# Task 8.14: Quiz Pages (List, Take, Result)

## Description

Create Quiz list page, quiz taking page with timer, and quiz result page.

## Priority
🔴 Critical — Core quiz feature

## Dependencies
- Task 8.10 (Layout), Task 8.6 (API Services)

## Files to Create

| File | Action |
|------|--------|
| `EnglishLearning.Web/src/pages/QuizListPage.tsx` | Create |
| `EnglishLearning.Web/src/pages/QuizTakePage.tsx` | Create |
| `EnglishLearning.Web/src/pages/QuizResultPage.tsx` | Create |

## Steps

### Step 1: QuizListPage
1. List of quizzes with difficulty, time limit
2. Filter by difficulty
3. "Start Quiz" button

### Step 2: QuizTakePage
1. Display questions one by one
2. Timer countdown
3. Answer selection (multiple choice / fill in blank)
4. Submit quiz

### Step 3: QuizResultPage
1. Score display
2. Review answers (correct/incorrect)
3. "Back to Quizzes" button

## Expected Code

```typescript
// QuizListPage.tsx
import { useQuery } from '@tanstack/react-query';
import { Link } from 'react-router-dom';
import { quizService } from '../services/quiz.service';
import MainLayout from '../components/layout/MainLayout';
import Card from '../components/ui/Card';
import Badge from '../components/ui/Badge';
import Button from '../components/ui/Button';
import { Clock, BookOpen } from 'lucide-react';

const difficultyMap: Record<number, string> = { 0: 'Beginner', 1: 'Intermediate', 2: 'Advanced' };
const difficultyVariant: Record<number, 'success' | 'warning' | 'danger'> = { 0: 'success', 1: 'warning', 2: 'danger' };

const QuizListPage: React.FC = () => {
  const { data, isLoading } = useQuery({
    queryKey: ['quizzes'],
    queryFn: () => quizService.getAll(1, 20),
  });

  const quizzes = data?.data || [];

  return (
    <MainLayout>
      <div className="space-y-6">
        <h1 className="text-2xl font-bold">Quizzes</h1>
        {isLoading ? (
          <div className="flex justify-center py-12"><div className="animate-spin rounded-full h-12 w-12 border-b-2 border-blue-600" /></div>
        ) : (
          <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-4">
            {quizzes.map((q: any) => (
              <Card key={q.id} className="flex flex-col">
                <div className="flex items-start justify-between">
                  <h3 className="font-semibold text-lg">{q.title}</h3>
                  <Badge variant={difficultyVariant[q.difficulty]}>{difficultyMap[q.difficulty]}</Badge>
                </div>
                {q.description && <p className="text-gray-600 text-sm mt-2">{q.description}</p>}
                <div className="flex items-center gap-4 mt-4 text-sm text-gray-500">
                  <span className="flex items-center gap-1"><Clock className="w-4 h-4" />{q.timeLimitMinutes} min</span>
                  <span className="flex items-center gap-1"><BookOpen className="w-4 h-4" />{q.questions?.length || 0} questions</span>
                </div>
                <Link to={`/quiz/${q.id}`} className="mt-4"><Button className="w-full">Start Quiz</Button></Link>
              </Card>
            ))}
          </div>
        )}
      </div>
    </MainLayout>
  );
};

export default QuizListPage;

// QuizTakePage.tsx
import { useQuery, useMutation } from '@tanstack/react-query';
import { useParams, useNavigate } from 'react-router-dom';
import { useState, useEffect } from 'react';
import useAuth from '../hooks/useAuth';
import { quizService } from '../services/quiz.service';
import MainLayout from '../components/layout/MainLayout';
import Card from '../components/ui/Card';
import Button from '../components/ui/Button';
import Badge from '../components/ui/Badge';
import { Clock, CheckCircle, XCircle } from 'lucide-react';

const QuizTakePage: React.FC = () => {
  const { id } = useParams<{ id: string }>();
  const navigate = useNavigate();
  const { user } = useAuth();
  const [currentQuestion, setCurrentQuestion] = useState(0);
  const [answers, setAnswers] = useState<Record<string, string>>({});
  const [timeLeft, setTimeLeft] = useState(0);

  const { data, isLoading } = useQuery({
    queryKey: ['quiz', id],
    queryFn: () => quizService.getById(id!),
  });

  const quiz = data?.data;
  const questions = quiz?.questions || [];
  const question = questions[currentQuestion];

  useEffect(() => {
    if (quiz) {
      setTimeLeft(quiz.timeLimitMinutes * 60);
      const timer = setInterval(() => {
        setTimeLeft(prev => {
          if (prev <= 1) {
            clearInterval(timer);
            handleSubmit();
            return 0;
          }
          return prev - 1;
        });
      }, 1000);
      return () => clearInterval(timer);
    }
  }, [quiz]);

  const submitMutation = useMutation({
    mutationFn: (data: any) => quizService.submitResult(data),
    onSuccess: (response) => {
      navigate(`/quiz/${id}/result`, { state: { result: response.data } });
    },
  });

  const handleSubmit = () => {
    if (!user || !quiz) return;
    submitMutation.mutate({
      quizId: quiz.id,
      userId: user.id,
      durationMinutes: Math.floor((quiz.timeLimitMinutes * 60 - timeLeft) / 60),
      answers: Object.entries(answers).map(([questionId, selectedChoiceId]) => ({
        questionId,
        selectedChoiceId: selectedChoiceId || null,
        answerText: null,
      })),
    });
  };

  const formatTime = (seconds: number) => {
    const mins = Math.floor(seconds / 60);
    const secs = seconds % 60;
    return `${mins}:${secs.toString().padStart(2, '0')}`;
  };

  if (isLoading) return <MainLayout><div className="flex justify-center py-12"><div className="animate-spin rounded-full h-12 w-12 border-b-2 border-blue-600" /></div></MainLayout>;
  if (!question) return null;

  return (
    <MainLayout>
      <div className="max-w-3xl mx-auto space-y-6">
        <div className="flex items-center justify-between">
          <Badge>{currentQuestion + 1} / {questions.length}</Badge>
          <div className="flex items-center gap-2 text-gray-600"><Clock className="w-5 h-5" />{formatTime(timeLeft)}</div>
        </div>

        <Card>
          <h2 className="text-xl font-semibold mb-6">{question.questionText}</h2>
          <div className="space-y-3">
            {question.choices?.map((choice: any) => (
              <button
                key={choice.id}
                onClick={() => setAnswers({ ...answers, [question.id]: choice.id })}
                className={`w-full text-left p-4 rounded-lg border-2 transition-colors ${
                  answers[question.id] === choice.id ? 'border-blue-500 bg-blue-50' : 'border-gray-200 hover:border-gray-300'
                }`}
              >
                {choice.choiceText}
              </button>
            ))}
          </div>
        </Card>

        <div className="flex justify-between">
          <Button variant="secondary" onClick={() => setCurrentQuestion(Math.max(0, currentQuestion - 1))} disabled={currentQuestion === 0}>Previous</Button>
          {currentQuestion < questions.length - 1 ? (
            <Button onClick={() => setCurrentQuestion(currentQuestion + 1)}>Next</Button>
          ) : (
            <Button onClick={handleSubmit} isLoading={submitMutation.isPending}>Submit Quiz</Button>
          )}
        </div>
      </div>
    </MainLayout>
  );
};

export default QuizTakePage;

// QuizResultPage.tsx
import { useLocation, Link } from 'react-router-dom';
import MainLayout from '../components/layout/MainLayout';
import Card from '../components/ui/Card';
import Button from '../components/ui/Button';
import Badge from '../components/ui/Badge';
import { CheckCircle, XCircle, Award } from 'lucide-react';

const QuizResultPage: React.FC = () => {
  const location = useLocation();
  const result = location.state?.result;

  if (!result) return <MainLayout><div className="text-center py-12">No result found</div></MainLayout>;

  const passed = result.score >= 50;

  return (
    <MainLayout>
      <div className="max-w-2xl mx-auto space-y-6">
        <Card className="text-center">
          <div className="flex justify-center mb-4">
            {passed ? <Award className="w-16 h-16 text-yellow-500" /> : <XCircle className="w-16 h-16 text-red-500" />}
          </div>
          <h1 className="text-3xl font-bold">{passed ? 'Congratulations!' : 'Keep Practicing!'}</h1>
          <p className="text-6xl font-bold text-blue-600 my-4">{result.score}%</p>
          <div className="grid grid-cols-3 gap-4 mt-6">
            <div><p className="text-2xl font-bold">{result.correctAnswers}</p><p className="text-sm text-gray-600">Correct</p></div>
            <div><p className="text-2xl font-bold">{result.totalQuestions - result.correctAnswers}</p><p className="text-sm text-gray-600">Wrong</p></div>
            <div><p className="text-2xl font-bold">{result.durationMinutes}</p><p className="text-sm text-gray-600">Minutes</p></div>
          </div>
        </Card>
        <Link to="/quiz"><Button className="w-full">Back to Quizzes</Button></Link>
      </div>
    </MainLayout>
  );
};

export default QuizResultPage;
```

## Verification

- [ ] Quiz list displays correctly
- [ ] Quiz taking with timer works
- [ ] Result page shows score

## Acceptance Criteria

- [ ] `QuizListPage` with quiz cards and filter
- [ ] `QuizTakePage` with question navigation, timer, answer selection
- [ ] `QuizResultPage` with score, correct/wrong count, time
- [ ] Auto-submit when timer expires
- [ ] Pass/fail indication
