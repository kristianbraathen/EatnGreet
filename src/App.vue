<script setup>  
    import MainPage from './components/MainPage.vue'
    import LoginPage from './components/LoginPage.vue'
    import NewRegister from './components/NewRegister.vue'
    import { ref } from 'vue'
    import MapComponent from './components/MapComponent.vue'
   
</script>

<template>
    <header>
        <img class="logo" src="@/assets/App logo1.png" alt="Logo">
        <input type="search" v-model="searchQuery" placeholder="Search" />
    </header>
    <nav class="navbar">
        <div class="navbar-item">
            <button @click="GoToProfile">P</button>
            <button @click="openNewRegModal">R</button>
            <NewRegister v-if="showNewRegModal" @close="showNewRegModal = false"></NewRegister>

            <button @click="GoToMain">H</button>
            <button @click="openLoginModal">L</button>
        </div>
    </nav>
    <main>
        <LoginPage v-if="showLoginModal" @close="showLoginModal = false" />
        <router-view />
        <div class="map-window">
            <MapComponent></MapComponent>
        </div>
    </main>

</template>

<script>

    export default {
        name: 'App',
        components: {
            MainPage,
            LoginPage,
            NewRegister,
            MapComponent
            
            
           
        },
        data() {
            return {
                searchQuery: '',
                showLoginModal: false,
                showNewRegModal: false,
            }
        },
        
        methods: {
           
            openLoginModal() {
                this.showLoginModal = true;
            },
            openNewRegModal() {
                this.showNewRegModal = true;
            },
            GoToMain(){
                this.$router.push('/MainPage')
            },
            GoToProfile() {
                this.$router.push('/MyProfile')
            }
        },
        
           
    }
</script>

    <style scoped>
        header {
            display: flex;
            flex-direction: column;
            align-items: center;
            justify-content: space-around;
            margin-bottom: 20px;
            
        }

        .logo {
            width: 100px; /* Adjust as needed */
            height: auto; /* Maintain aspect ratio */
            margin-bottom: 20px;
        }


        .navbar {
            display: flex;
            justify-content: space-around;
            margin-bottom: 20px;
            margin-top: 40px;
        }
        .navbar-item {
            display: flex;
            gap: 1rem;
        }
        button {
            background-color: white;
            color: orange;
            border: none;
            padding: 20px 20px;
            border-radius: 50%;
            cursor: pointer;
            font-size:x-small;
        }
        .map-window {
            width: 600px;
            height: 400px;
            border: 1px solid #000;
            margin: 0 auto;
        }
    </style>
