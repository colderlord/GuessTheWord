import React, {Component, ReactElement} from 'react';
import { Container } from 'reactstrap';
import { NavMenu } from './NavMenu';

export interface LayoutProps {
    children?: any
}

export class Layout extends Component<LayoutProps> {
  static displayName = Layout.name;

  render () {
    return (
      <div>
        <NavMenu />
        <Container>
          {this.props.children}
        </Container>
      </div>
    );
  }
}
