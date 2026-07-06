# Task 8.13: Vocabulary Pages

## Description

Create Vocabulary list page with search, filter, and detail/flashcard view.

## Priority
🔴 Critical — Core learning feature

## Dependencies
- Task 8.10 (Layout), Task 8.6 (API Services)

## Files to Create

| File | Action |
|------|--------|
| `EnglishLearning.Web/src/pages/VocabularyPage.tsx` | Create |
| `EnglishLearning.Web/src/pages/VocabularyDetailPage.tsx` | Create |

## Steps

### Step 1: VocabularyPage
1. Search bar, difficulty filter
2. Card grid showing vocabulary words
3. Pagination

### Step 2: VocabularyDetailPage
1. Flashcard style view
2. Word, definition, example, part of speech
3. Difficulty badge

## Expected Code

```typescript
// VocabularyPage.tsx
import { useQuery } from '@tanstack/react-query';
import { useState } from 'react';
import { Link } from 'react-router-dom';
import { vocabularyService } from '../services/vocabulary.service';
import MainLayout from '../components/layout/MainLayout';
import Card from '../components/ui/Card';
import Badge from '../components/ui/Badge';
import Input from '../components/ui/Input';
import Button from '../components/ui/Button';
import { Search, BookOpen } from 'lucide-react';

const difficultyMap: Record<number, string> = { 0: 'Beginner', 1: 'Intermediate', 2: 'Advanced' };
const difficultyVariant: Record<number, 'success' | 'warning' | 'danger'> = { 0: 'success', 1: 'warning', 2: 'danger' };

const VocabularyPage: React.FC = () => {
  const [search, setSearch] = useState('');
  const [difficulty, setDifficulty] = useState<number | undefined>(undefined);
  const [pageNumber, setPageNumber] = useState(1);

  const { data, isLoading } = useQuery({
    queryKey: ['vocabularies', pageNumber, difficulty],
    queryFn: () => vocabularyService.getAll(pageNumber, 12, difficulty),
  });

  const vocabularies = data?.data || [];

  return (
    <MainLayout>
      <div className="space-y-6">
        <h1 className="text-2xl font-bold">Vocabulary</h1>

        <div className="flex gap-4">
          <Input placeholder="Search words..." value={search} onChange={e => setSearch(e.target.value)} className="max-w-md" />
          <select value={difficulty ?? ''} onChange={e => setDifficulty(e.target.value ? Number(e.target.value) : undefined)} className="px-3 py-2 border rounded-lg">
            <option value="">All Levels</option>
            <option value="0">Beginner</option>
            <option value="1">Intermediate</option>
            <option value="2">Advanced</option>
          </select>
        </div>

        {isLoading ? (
          <div className="flex justify-center py-12"><div className="animate-spin rounded-full h-12 w-12 border-b-2 border-blue-600" /></div>
        ) : (
          <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-4">
            {vocabularies.map((v: any) => (
              <Link key={v.id} to={`/vocabulary/${v.id}`}>
                <Card className="hover:shadow-lg transition-shadow h-full">
                  <div className="flex items-start justify-between">
                    <h3 className="text-lg font-semibold">{v.word}</h3>
                    <Badge variant={difficultyVariant[v.difficulty]}>{difficultyMap[v.difficulty]}</Badge>
                  </div>
                  <p className="text-gray-600 mt-2 text-sm line-clamp-2">{v.definition}</p>
                  {v.partOfSpeech && <p className="text-xs text-gray-400 mt-2">{v.partOfSpeech}</p>}
                </Card>
              </Link>
            ))}
          </div>
        )}
      </div>
    </MainLayout>
  );
};

export default VocabularyPage;

// VocabularyDetailPage.tsx
import { useQuery } from '@tanstack/react-query';
import { useParams, Link } from 'react-router-dom';
import { vocabularyService } from '../services/vocabulary.service';
import MainLayout from '../components/layout/MainLayout';
import Card from '../components/ui/Card';
import Badge from '../components/ui/Badge';
import Button from '../components/ui/Button';
import { ArrowLeft } from 'lucide-react';

const VocabularyDetailPage: React.FC = () => {
  const { id } = useParams<{ id: string }>();
  const [isFlipped, setIsFlipped] = useState(false);

  const { data, isLoading } = useQuery({
    queryKey: ['vocabulary', id],
    queryFn: () => vocabularyService.getById(id!),
  });

  const vocab = data?.data;

  return (
    <MainLayout>
      <div className="max-w-2xl mx-auto space-y-6">
        <Link to="/vocabulary"><Button variant="ghost"><ArrowLeft className="w-4 h-4 mr-2" />Back</Button></Link>

        {isLoading ? (
          <div className="flex justify-center py-12"><div className="animate-spin rounded-full h-12 w-12 border-b-2 border-blue-600" /></div>
        ) : vocab && (
          <Card className="cursor-pointer" onClick={() => setIsFlipped(!isFlipped)}>
            <div className="min-h-[200px] flex flex-col items-center justify-center text-center">
              {!isFlipped ? (
                <>
                  <h2 className="text-3xl font-bold">{vocab.word}</h2>
                  <Badge variant="info" className="mt-2">{vocab.partOfSpeech}</Badge>
                  <p className="text-gray-500 mt-4 text-sm">Click to reveal definition</p>
                </>
              ) : (
                <>
                  <p className="text-xl">{vocab.definition}</p>
                  {vocab.example && <p className="text-gray-600 mt-4 italic">"{vocab.example}"</p>}
                  <p className="text-gray-500 mt-4 text-sm">Click to see word again</p>
                </>
              )}
            </div>
          </Card>
        )}
      </div>
    </MainLayout>
  );
};

export default VocabularyDetailPage;
```

## Verification

- [ ] Vocabulary list displays correctly
- [ ] Search and filter work
- [ ] Flashcard view works

## Acceptance Criteria

- [ ] `VocabularyPage` with search, difficulty filter, card grid
- [ ] `VocabularyDetailPage` with flashcard (click to flip)
- [ ] Difficulty badges with colors
- [ ] Pagination support
