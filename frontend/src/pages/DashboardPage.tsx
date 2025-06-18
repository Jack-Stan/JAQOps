import React from 'react';
import { useAuthStore } from '../store/auth.store';

const DashboardPage: React.FC = () => {
  const { user, logout } = useAuthStore();

  return (
    <div className="min-h-screen bg-gray-100">
      <nav className="bg-white shadow-sm">
        <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
          <div className="flex justify-between h-16">
            <div className="flex items-center">
              <h1 className="text-xl font-bold">JAQOps</h1>
            </div>
            <div className="flex items-center">
              <span className="mr-4">Welcome, {user?.firstName} {user?.lastName}</span>
              <button 
                onClick={logout}
                className="px-4 py-2 bg-red-600 text-white rounded-md hover:bg-red-700"
              >
                Logout
              </button>
            </div>
          </div>
        </div>
      </nav>

      <div className="max-w-7xl mx-auto py-6 sm:px-6 lg:px-8">
        <div className="px-4 py-6 sm:px-0">
          <div className="border-4 border-dashed border-gray-200 rounded-lg p-4 min-h-screen">
            <h2 className="text-2xl font-bold mb-4">Dashboard</h2>
            
            <div className="grid grid-cols-1 md:grid-cols-3 gap-4 mb-8">
              <div className="bg-white p-4 rounded-lg shadow">
                <h3 className="text-lg font-medium">Active Jobs</h3>
                <p className="text-3xl font-bold">0</p>
              </div>
              <div className="bg-white p-4 rounded-lg shadow">
                <h3 className="text-lg font-medium">Customers</h3>
                <p className="text-3xl font-bold">0</p>
              </div>
              <div className="bg-white p-4 rounded-lg shadow">
                <h3 className="text-lg font-medium">This Month Revenue</h3>
                <p className="text-3xl font-bold">€0.00</p>
              </div>
            </div>
            
            <div className="bg-white p-4 rounded-lg shadow mb-8">
              <h3 className="text-lg font-medium mb-4">Recent Jobs</h3>
              <div className="text-center text-gray-500 py-8">
                No jobs found. Create your first job to get started.
              </div>
            </div>
            
            <div className="bg-white p-4 rounded-lg shadow">
              <h3 className="text-lg font-medium mb-4">Recent Invoices</h3>
              <div className="text-center text-gray-500 py-8">
                No invoices found. Create your first invoice to get started.
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default DashboardPage;
