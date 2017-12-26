import React from 'react';
import ReactDOM from 'react-dom';
// TODO: should change this to proper antd/lib/** imports to reduce dist.js size
import { Menu, Icon, Avatar, Spin } from 'antd';
import enUS from 'antd/lib/locale-provider/en_US';

const SubMenu = Menu.SubMenu;
const MenuItemGroup = Menu.ItemGroup;

class Sider extends React.Component {
    handleClick() {
        console.log("Click!");
    }

    render() {
        return (
            <Menu
                onClick={this.handleClick}
                style={{ width: 240, minHeight: '100vh' }}
                defaultSelectedKeys={['0']}
                defaultOpenKeys={['sub1']}
                mode="inline"
                theme='dark'
            >
                <Menu.Item key="0"><Icon type="appstore" />Dashboard</Menu.Item>
            </Menu>
        );
        /*
                <SubMenu key="sub1" title={<span><Icon type="mail" /><span>Navigation One</span></span>}>
                    <MenuItemGroup key="g1" title="Item 1">
                        <Menu.Item key="1">Option 1</Menu.Item>
                        <Menu.Item key="2">Option 2</Menu.Item>
                    </MenuItemGroup>
                    <MenuItemGroup key="g2" title="Item 2">
                        <Menu.Item key="3">Option 3</Menu.Item>
                        <Menu.Item key="4">Option 4</Menu.Item>
                    </MenuItemGroup>
                </SubMenu>
                <SubMenu key="sub2" title={<span><Icon type="appstore" /><span>Navigation Two</span></span>}>
                    <Menu.Item key="5">Option 5</Menu.Item>
                    <Menu.Item key="6">Option 6</Menu.Item>
                    <SubMenu key="sub3" title="Submenu">
                        <Menu.Item key="7">Option 7</Menu.Item>
                        <Menu.Item key="8">Option 8</Menu.Item>
                    </SubMenu>
                </SubMenu>
                <SubMenu key="sub4" title={<span><Icon type="setting" /><span>Navigation Three</span></span>}>
                    <Menu.Item key="9">Option 9</Menu.Item>
                    <Menu.Item key="10">Option 10</Menu.Item>
                    <Menu.Item key="11">Option 11</Menu.Item>
                    <Menu.Item key="12">Option 12</Menu.Item>
                </SubMenu>
                */
    }
}

class Header extends React.Component {
    render() {
        return (
            <div style={{ marginLeft: 'auto', marginRight: '16px', 'display': 'flex', justifyContent: 'center', alignItems: 'center' }}>
                <span style={{ paddingRight: '12px' }}> Hello Dr. H. Timorabadi! </span>
                <Avatar size='large' src="http://news.engineering.utoronto.ca/files/2016/10/hamidcircle.png" />
            </div>
        );
    }
}

var siderMountNode = document.getElementById("react-sidemenu-target");
var headerMountNode = document.getElementById("topmenu-container");
var spinnerNode = document.getElementById("main-spinner");
ReactDOM.render(<Spin style={{ position: 'relative', top: '-32px', transform: 'scale(2)' }} size="large" />, spinnerNode);
ReactDOM.render(<Sider />, siderMountNode);
ReactDOM.render(<Header />, headerMountNode);