import axios from 'axios';

const API_URL = process.env.REACT_APP_API_URL || 'http://localhost:5000/api';

export interface LoginRequest {
  email: string;
  password: string;
}

export interface User {
  email: string;
  firstName: string;
  lastName: string;
  roles: string[];
}

export interface LoginResponse {
  token: string;
  user: User;
}

class AuthService {
  login = async (credentials: LoginRequest): Promise<LoginResponse> => {
    try {
      const response = await axios.post<LoginResponse>(`${API_URL}/auth/login`, credentials);
      
      // Save token to localStorage
      localStorage.setItem('token', response.data.token);
      localStorage.setItem('user', JSON.stringify(response.data.user));
      
      // Set token in axios headers for all future requests
      axios.defaults.headers.common['Authorization'] = `Bearer ${response.data.token}`;
      
      return response.data;
    } catch (error) {
      console.error('Login failed:', error);
      throw error;
    }
  };

  logout = (): void => {
    // Remove token and user from localStorage
    localStorage.removeItem('token');
    localStorage.removeItem('user');
    
    // Remove token from axios headers
    delete axios.defaults.headers.common['Authorization'];
  };

  getCurrentUser = (): User | null => {
    const userStr = localStorage.getItem('user');
    if (userStr) {
      return JSON.parse(userStr) as User;
    }
    return null;
  };

  isAuthenticated = (): boolean => {
    return !!localStorage.getItem('token');
  };

  hasRole = (role: string): boolean => {
    const user = this.getCurrentUser();
    return user ? user.roles.includes(role) : false;
  };

  getToken = (): string | null => {
    return localStorage.getItem('token');
  };

  // Setup method to initialize axios with token if it exists
  setupAxios = (): void => {
    const token = this.getToken();
    if (token) {
      axios.defaults.headers.common['Authorization'] = `Bearer ${token}`;
    }
  };
}

export default new AuthService();
