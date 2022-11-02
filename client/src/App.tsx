import React from 'react';
import Layout from './layout/Layout';
import Application from './components/Application';
import './App.css';

function App() {
  return (
    <div className="App">
      <Layout>
          <Application />
      </Layout>
    </div>
  );
}

export default App;
