<template>
    <div>
        <h2>Login Page</h2>
        <button class="close-button" @click="closeLoginModal">X</button>
        <form class="form" @submit.prevent="login">
            <label for="username">Username/Mail:</label>
            <input type="text" id="username" name="username" v-model="username">
            <label for="password">Password:</label>
            <input type="password" id="password" name="password" v-model="password">
            <button type="submit">Login</button>
        </form>
       
    </div>
</template>
<script lang="js">
    import { defineComponent, ref } from 'vue'
    import NewRegister from './NewRegister.vue'
   

    export default defineComponent({
        name: 'login',
        components: {    
            NewRegister,
           
        },
        data() {
            return {
                username: '',
                password: '',
                showNewRegModal: false,
                
                
                
            };
        },
        methods: {
            async login() {
                try {
                    const response = await fetch('https://localhost:7218/api/Login/Login', {
                        method: 'POST',
                        headers: {
                            'Content-Type': 'application/json',
                        },
                        body: JSON.stringify({
                            email: this.username,
                            password: this.password,
                        }),
                    });

                    if (response.ok) {
                        const data = await response.json();
                        localStorage.setItem('user', JSON.stringify(data.user));
                        console.log(data.user)
                        alert('Login')
                        // Login was successful
                        this.$emit('login-success')
                        this.closeLoginModal()
                        this.$router.push('/MyProfile')

                    } else {
                        alert('Something went wrong!');
                        // Login failed
                        // Show an error message
                    }
                } catch (error) {
                    console.error('Error:', error);
                }
                },
                closeLoginModal() {
                     this.$emit('close');       
            },
            openNewRegModal() {
                this.showNewRegModal = true;
                console.log('openNewRegModal called, showNewRegModal is now', this.showNewRegModal);
            },
            
        },
      
    })
</script>


<style>
    h2 {
        color: #333;
    }
    .modal {
        position: relative;
        z-index: 1;
        left: 0;
        top: 0;
        width: 100%;
        height: 100%;
        overflow: auto;
        background-color: orange;
        
    }
    .close-button {
        position: absolute;
        top: 10px;
        right: 10px;
        /* Other styles... */
    }


    form {
        display: grid;
        gap: 1rem;
    }

    label {
        font-weight: bold;
    }

    input {
        padding: 0.5rem;
        border: 1px solid #ccc;
        border-radius: 0.5rem;
    }

    button {
        padding: 0.5rem 1rem;
        background-color: #333;
        color: white;
        border: none;
        border-radius: 0.5rem;
        cursor: pointer;
    }

        button:hover {
            background-color: #555;
        }

        button:active {
            background-color: #777;
        }

        button:focus {
            outline: none;
        }
</style>

