import React, { Component } from 'react';
import { Route, BrowserRouter } from 'react-router-dom';
import AppRoutes from './AppRoutes';
import { Layout } from './components/Layout';
import './custom.css';
import {StorageService} from "./services/storage.service";
import {SecurityService} from "./services/security.service";
import {ConfigurationService} from "./services/configuration.service";

const storageService = new StorageService();
const configurationService = new ConfigurationService(storageService);
const securityService = new SecurityService(configurationService, storageService)


export default class App extends Component {
  static displayName = App.name;
  
  componentDidMount() {
      configurationService.load();
  }

  render() {
    return (
      <Layout>
        <BrowserRouter>
            {/*<Route path={'/'} component={Home} />*/}
            {/*<Route path={'/counter'} component={Counter} />*/}
            {/*<Route path={'/fetch-data'} component={FetchData} />*/}
          {AppRoutes.map((route, index) => {
            const { element, ...rest } = route;
              return <Route key={index} {...rest} exact component={element} />;
          })}
        </BrowserRouter>
      </Layout>
    );
  }
}
