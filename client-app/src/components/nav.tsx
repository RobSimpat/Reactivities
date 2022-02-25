import { observer } from "mobx-react-lite";
import React from "react";
import { Link, NavLink } from "react-router-dom";
import { Button, Container, Dropdown, Menu, MenuItem } from "semantic-ui-react";
import { useStore } from "../app/stores/store";





export default observer(function NavBar() {
    const { userStore: { user, logout } } = useStore();


    return (
        <Menu inverted fixed='top'>
            <Container>
                <Menu.Item className="nav-link" position='left' as={NavLink} exact to='/' header>
                    Home</Menu.Item>

                <Menu.Item>
                    <Dropdown>
                        <Dropdown.Menu>
                            <Dropdown.Item as={Link} to='/profile' text='My Profile' icon='user' className="nav-link"/>
                            <Dropdown.Item onClick={logout} text='Logout' icon='power' className="nav-link"/>

                            
                            

                            
                        </Dropdown.Menu>
                    </Dropdown>
                </Menu.Item>

{/* 
                <Menu.Item position='right'>
                    <Dropdown pointing='top left' text={user?.displayName}>
                        <Dropdown.Menu>
                            <Dropdown.Item as={Link} to='/profile' text='My Profile' icon='user' className="nav-link" />
                            <Dropdown.Item onClick={logout} text='Logout' icon='power' className="nav-link" />
                            <Dropdown.Item as={Link} to={`/profile/${user?.userName}`} />

                        </Dropdown.Menu>



                    </Dropdown>

                </Menu.Item> */}

            </Container>
        </Menu>
    )
})


