import { observer } from "mobx-react-lite";
import { Link } from "react-router-dom";
import { Button, Container, Header, Menu, Segment } from "semantic-ui-react";
import { useStore } from "../../app/stores/store";
import React from "react";
import LoginForm from "../users/LoginForm";


export default observer(function HomePage() {
    const { userStore,modalStore } = useStore();
    

    return (

        <Segment inverted textAlign='center' vertical className="masthead">
            <Container style={{ marginTop: '7em' }}>
                <Header>
                    <h1>GG8 Game Trackin App</h1>
                </Header>
                        
                   


            {userStore.isLoggedIn ? (
                <>
                    <Header as='h2' inverted content='Welcome' />
                    <Button as={Link} to='/register' size="huge" inverted>
                        Register

                    </Button>
                </>
            ) : (
                <Button as={Link} to='/login'  size="huge" inverted>
                    Login
                    {/* onClick={()=> modalStore.openModal(<LoginForm/>)} */}
                    {/* */}

                </Button>
            )}


            </Container>

        </Segment >





    )
})