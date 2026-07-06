# Task 8.2: Configure TailwindCSS + PostCSS

## Description

Install and configure TailwindCSS for the React frontend project.

## Priority
🔴 Critical — Styling framework

## Dependencies
- Task 8.1 (React project created)

## Files to Create/Edit

| File | Action |
|------|--------|
| `EnglishLearning.Web/tailwind.config.js` | Create |
| `EnglishLearning.Web/postcss.config.js` | Create |
| `EnglishLearning.Web/src/index.css` | Edit |

## Steps

### Step 1: Install TailwindCSS
1. `npm install -D tailwindcss postcss autoprefixer`
2. `npx tailwindcss init -p`

### Step 2: Configure tailwind.config.js
1. Set content paths: `['./index.html', './src/**/*.{js,ts,jsx,tsx}']`

### Step 3: Update index.css
1. Add Tailwind directives: `@tailwind base; @tailwind components; @tailwind utilities;`

### Step 4: Verify
1. Run `npm run dev` and check if Tailwind classes work

## Expected Code

```js
// tailwind.config.js
/** @type {import('tailwindcss').Config} */
export default {
  content: [
    "./index.html",
    "./src/**/*.{js,ts,jsx,tsx}",
  ],
  theme: {
    extend: {},
  },
  plugins: [],
}

// postcss.config.js
export default {
  plugins: {
    tailwindcss: {},
    autoprefixer: {},
  },
}

// src/index.css
@tailwind base;
@tailwind components;
@tailwind utilities;
```

## Verification

- [ ] TailwindCSS installed and configured
- [ ] `npm run dev` works
- [ ] Tailwind classes work in components

## Acceptance Criteria

- [ ] TailwindCSS installed as dev dependency
- [ ] `tailwind.config.js` configured with correct content paths
- [ ] `postcss.config.js` created
- [ ] `index.css` has Tailwind directives
- [ ] Tailwind classes work in components
