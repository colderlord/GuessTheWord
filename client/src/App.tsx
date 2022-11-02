import React from 'react';
import { observer } from 'mobx-react';
import Layout from './layout/Layout';
import Application from './components/Application';
import './App.css';
import { Storage } from "./storage/Storage";

const storage = new Storage();

function App() {
  return (
    <div className="App">
      <Layout storage={storage}>
          <Application storage={storage} />
      </Layout>
    </div>
  );
}

export default observer(App);
