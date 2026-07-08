import { useState, useEffect } from 'react'
import { vocabularyService } from '@/services/vocabularyService'
import { Vocabulary, DifficultyLevel } from '@/types'
import { Plus, Edit, Trash2, X } from 'lucide-react'

export default function AdminVocabularyPage() {
  const [vocabularies, setVocabularies] = useState<Vocabulary[]>([])
  const [showForm, setShowForm] = useState(false)
  const [editingVocab, setEditingVocab] = useState<Vocabulary | null>(null)
  const [form, setForm] = useState({
    word: '',
    definition: '',
    example: '',
    difficulty: 'Beginner' as DifficultyLevel,
  })

  useEffect(() => {
    vocabularyService
      .getAll(1, 100)
      .then((data) => setVocabularies(data.items || []))
      .catch((error) => {
        console.error('Failed to load vocabularies:', error)
        setVocabularies([])
      })
  }, [])

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault()
    if (editingVocab) {
      await vocabularyService.update(editingVocab.id, form)
    } else {
      await vocabularyService.create(form)
    }
    resetForm()
    // Reload list
    vocabularyService
      .getAll(1, 100)
      .then((data) => setVocabularies(data.items || []))
      .catch((error) => {
        console.error('Failed to reload vocabularies:', error)
      })
  }

  const handleEdit = (v: Vocabulary) => {
    setEditingVocab(v)
    setForm({
      word: v.word,
      definition: v.definition,
      example: v.example || '',
      difficulty: v.difficulty,
    })
    setShowForm(true)
  }

  const handleDelete = async (id: string) => {
    if (!confirm('Are you sure you want to delete this word?')) return
    await vocabularyService.delete(id)
    // Reload list
    vocabularyService
      .getAll(1, 100)
      .then((data) => setVocabularies(data.items || []))
      .catch((error) => {
        console.error('Failed to reload vocabularies:', error)
      })
  }

  const resetForm = () => {
    setShowForm(false)
    setEditingVocab(null)
    setForm({ word: '', definition: '', example: '', difficulty: 'Beginner' })
  }

  return (
    <div>
      <div className="mb-6 flex items-center justify-between">
        <div>
          <h1 className="text-2xl font-bold text-gray-900">Manage Vocabulary</h1>
          <p className="text-gray-600">Add and edit vocabulary words</p>
        </div>
        <button className="btn btn-primary" onClick={() => setShowForm(true)}>
          <Plus className="mr-2 h-4 w-4" />
          New Word
        </button>
      </div>

      {/* Form Modal */}
      {showForm && (
        <div className="fixed inset-0 z-50 flex items-center justify-center bg-black/50 p-4">
          <div className="card w-full max-w-lg">
            <div className="mb-4 flex items-center justify-between">
              <h2 className="text-lg font-semibold text-gray-900">
                {editingVocab ? 'Edit Word' : 'Add New Word'}
              </h2>
              <button onClick={resetForm} className="text-gray-400 hover:text-gray-600">
                <X className="h-5 w-5" />
              </button>
            </div>

            <form onSubmit={handleSubmit}>
              <div className="mb-4">
                <label className="mb-1 block text-sm font-medium text-gray-700">Word</label>
                <input
                  type="text"
                  className="input"
                  value={form.word}
                  onChange={(e) => setForm({ ...form, word: e.target.value })}
                  required
                />
              </div>

              <div className="mb-4">
                <label className="mb-1 block text-sm font-medium text-gray-700">Definition</label>
                <textarea
                  className="input"
                  rows={3}
                  value={form.definition}
                  onChange={(e) => setForm({ ...form, definition: e.target.value })}
                  required
                />
              </div>

              <div className="mb-4">
                <label className="mb-1 block text-sm font-medium text-gray-700">Example</label>
                <textarea
                  className="input"
                  rows={2}
                  value={form.example}
                  onChange={(e) => setForm({ ...form, example: e.target.value })}
                />
              </div>

              <div className="mb-6">
                <label className="mb-1 block text-sm font-medium text-gray-700">Difficulty</label>
                <select
                  className="input"
                  value={form.difficulty}
                  onChange={(e) => setForm({ ...form, difficulty: e.target.value as DifficultyLevel })}
                >
                  <option value="Beginner">Beginner</option>
                  <option value="Intermediate">Intermediate</option>
                  <option value="Advanced">Advanced</option>
                </select>
              </div>

              <div className="flex justify-end gap-3">
                <button type="button" className="btn btn-secondary" onClick={resetForm}>
                  Cancel
                </button>
                <button type="submit" className="btn btn-primary">
                  {editingVocab ? 'Update' : 'Add'}
                </button>
              </div>
            </form>
          </div>
        </div>
      )}

      {/* Vocabulary List */}
      <div className="space-y-3">
        {vocabularies.map((v) => (
          <div key={v.id} className="card flex items-center justify-between">
            <div>
              <h3 className="font-semibold text-gray-900">{v.word}</h3>
              <p className="text-sm text-gray-500">{v.definition}</p>
            </div>
            <div className="flex items-center gap-2">
              <span className="badge badge-primary">{v.difficulty}</span>
              <button className="btn btn-secondary" onClick={() => handleEdit(v)}>
                <Edit className="h-4 w-4" />
              </button>
              <button className="btn btn-danger" onClick={() => handleDelete(v.id)}>
                <Trash2 className="h-4 w-4" />
              </button>
            </div>
          </div>
        ))}
      </div>
    </div>
  )
}
