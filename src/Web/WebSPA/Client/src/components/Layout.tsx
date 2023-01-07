import React, {Component, ReactElement} from 'react';
import { Container } from 'reactstrap';
import { NavMenu } from './NavMenu';
import {GameInfoService} from "../services/gameInfo.service";

export interface LayoutProps {
    children?: any
    gameInfoService: GameInfoService
}

export class Layout extends Component<LayoutProps> {
  static displayName = Layout.name;

  render () {
    return (
      <div>
        <NavMenu gameInfoService={this.props.gameInfoService}/>
        <Container>
          {this.props.children}
        </Container>
      </div>
    );
  }
}
