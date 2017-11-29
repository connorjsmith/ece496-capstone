import React from 'react';

export class CardContainer extends React.Component {
    flexboxWrap(el) {
        return (
            <div className="flex-container-el">
                {el}
            </div>);
    }

    render() {
        console.log(this.props.children);
        return (
            <div className="flex-container">
                {React.Children.map(this.props.children, this.flexboxWrap)}
            </div>);
    }
}