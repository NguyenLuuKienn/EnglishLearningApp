# Task 8.11: Login & Register Pages

## Description

Create Login and Register pages with form validation.

## Priority
🔴 Critical — Authentication UI

## Dependencies
- Task 8.9 (UI Components), Task 8.7 (Auth Context)

## Files to Create

| File | Action |
|------|--------|
| `EnglishLearning.Web/src/pages/LoginPage.tsx` | Create |
| `EnglishLearning.Web/src/pages/RegisterPage.tsx` | Create |

## Expected Code

```typescript
// LoginPage.tsx
import { useState } from 'react';
import { useNavigate, Link, useLocation } from 'react-router-dom';
import { useForm } from 'react-hook-form';
import useAuth from '../hooks/useAuth';
import Input from '../components/ui/Input';
import Button from '../components/ui/Button';

interface LoginForm {
  username: string;
  password: string;
}

const LoginPage: React.FC = () => {
  const { login } = useAuth();
  const navigate = useNavigate();
  const location = useLocation();
  const [error, setError] = useState('');
  const [isLoading, setIsLoading] = useState(false);

  const { register, handleSubmit, formState: { errors } } = useForm<LoginForm>();

  const onSubmit = async (data: LoginForm) => {
    setIsLoading(true);
    setError('');
    try {
      await login(data.username, data.password);
      const from = (location.state as any)?.from?.pathname || '/dashboard';
      navigate(from, { replace: true });
    } catch (err: any) {
      setError(err.response?.data?.message || 'Login failed');
    } finally {
      setIsLoading(false);
    }
  };

  return (
    <div className="min-h-screen flex items-center justify-center bg-gray-50">
      <div className="max-w-md w-full space-y-8 p-8 bg-white rounded-xl shadow-lg">
        <div className="text-center">
          <h2 className="text-3xl font-bold text-gray-900">Welcome Back</h2>
          <p className="mt-2 text-gray-600">Sign in to your account</p>
        </div>
        <form onSubmit={handleSubmit(onSubmit)} className="space-y-6">
          <Input label="Username" {...register('username', { required: 'Username is required' })} error={errors.username?.message} />
          <Input label="Password" type="password" {...register('password', { required: 'Password is required' })} error={errors.password?.message} />
          {error && <p className="text-red-600 text-sm">{error}</p>}
          <Button type="submit" isLoading={isLoading} className="w-full">Sign In</Button>
        </form>
        <p className="text-center text-sm text-gray-600">
          Don't have an account? <Link to="/register" className="text-blue-600 hover:underline">Register</Link>
        </p>
      </div>
    </div>
  );
};

export default LoginPage;

// RegisterPage.tsx
import { useState } from 'react';
import { useNavigate, Link } from 'react-router-dom';
import { useForm } from 'react-hook-form';
import useAuth from '../hooks/useAuth';
import Input from '../components/ui/Input';
import Button from '../components/ui/Button';

interface RegisterForm {
  username: string;
  email: string;
  password: string;
  confirmPassword: string;
}

const RegisterPage: React.FC = () => {
  const { register: registerUser } = useAuth();
  const navigate = useNavigate();
  const [error, setError] = useState('');
  const [isLoading, setIsLoading] = useState(false);

  const { register, handleSubmit, watch, formState: { errors } } = useForm<RegisterForm>();

  const onSubmit = async (data: RegisterForm) => {
    setIsLoading(true);
    setError('');
    try {
      await registerUser(data.username, data.email, data.password);
      navigate('/login', { state: { message: 'Registration successful! Please login.' } });
    } catch (err: any) {
      setError(err.response?.data?.message || 'Registration failed');
    } finally {
      setIsLoading(false);
    }
  };

  return (
    <div className="min-h-screen flex items-center justify-center bg-gray-50">
      <div className="max-w-md w-full space-y-8 p-8 bg-white rounded-xl shadow-lg">
        <div className="text-center">
          <h2 className="text-3xl font-bold text-gray-900">Create Account</h2>
          <p className="mt-2 text-gray-600">Start your English learning journey</p>
        </div>
        <form onSubmit={handleSubmit(onSubmit)} className="space-y-4">
          <Input label="Username" {...register('username', { required: 'Username is required', minLength: { value: 3, message: 'Min 3 characters' } })} error={errors.username?.message} />
          <Input label="Email" type="email" {...register('email', { required: 'Email is required', pattern: { value: /\S+@\S+\.\S+/, message: 'Invalid email' } })} error={errors.email?.message} />
          <Input label="Password" type="password" {...register('password', { required: 'Password is required', minLength: { value: 6, message: 'Min 6 characters' } })} error={errors.password?.message} />
          <Input label="Confirm Password" type="password" {...register('confirmPassword', { validate: val => val === watch('password') || 'Passwords do not match' })} error={errors.confirmPassword?.message} />
          {error && <p className="text-red-600 text-sm">{error}</p>}
          <Button type="submit" isLoading={isLoading} className="w-full">Create Account</Button>
        </form>
        <p className="text-center text-sm text-gray-600">
          Already have an account? <Link to="/login" className="text-blue-600 hover:underline">Sign In</Link>
        </p>
      </div>
    </div>
  );
};

export default RegisterPage;
```

## Verification

- [ ] Login page works with auth context
- [ ] Register page validates input
- [ ] Redirect after successful login/register

## Acceptance Criteria

- [ ] `LoginPage` with username/password form
- [ ] `RegisterPage` with username/email/password/confirmPassword form
- [ ] Form validation (required, minLength, email pattern, password match)
- [ ] Error display from API
- [ ] Redirect after success
- [ ] Link between login/register pages
