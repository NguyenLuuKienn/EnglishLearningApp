export interface Vocabulary {
  id: string
  word: string
  definition: string
  example?: string
  difficulty: DifficultyLevel
  createdAt: string
}

export type DifficultyLevel = 'Beginner' | 'Intermediate' | 'Advanced'
