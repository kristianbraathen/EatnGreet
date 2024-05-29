<template>
    <div class="card-container">
        <div class="card" v-for="(entry, index) in entries" :key="index">
            <img v-if="entry.vegetarian" src="@/assets/EatNGreet Food1.jpg" alt="Vegetarian Food">
            <img v-else src="@/assets/EatNGreet Food2.jpg" alt="Non-Vegetarian Food">

            <h3>{{ entry.description }}</h3>
            <label for="allergens">Allergener:</label>
            <p>{{ entry.allergens }}</p>
            <label for="time">Tid:</label>
            <p>{{ entry.time }}</p>
            <label for="date">Dato:</label>
            <p>{{ entry.date }}</p>
            <div class="host">               
                <p v-if="user[entry.userRegId]">{{ user[entry.userRegId].firstName }}{{ user[entry.userRegId].lastName }}</p>
                <p v-if="user[entry.userRegId]">{{ user[entry.userRegId].address }}</p>
                <p v-if="user[entry.userRegId]">{{ user[entry.userRegId].city }}</p>
            </div>
        </div>
    </div>
</template>
<script lang="js">
    import { defineComponent } from 'vue';
    export default defineComponent({
        components: {
        },
        directives: {
        },
        filters: {
        },
        props: {
        },
        data() {
            return {
                entries: [],
                user: {},
                loggedIn: false,
            }
        },
        computed: {
        },
        watch: {
        },
        beforeCreate() {
        },
        created() {
            this.showEntries();
            
        },
        beforeMount() {
        },
        mounted() {
        },
        updated() {
        },
        activated() {
        },
        deactivated() {
        },
        beforeDestroy() {
        },
        destroyed() {
        },
        methods: {
            async showEntries() {
                try {
                    const response = await fetch('https://localhost:7218/api/Newentry');
                    const data = await response.json();
                    this.entries = data;
                    console.log(this.entries);

                    for (let entry of this.entries) {
                        await this.getUserRegistration(entry.userRegId);
                        console.log(entry.userRegId)
                    }
                }
                catch (error) {
                    console.error(error);
                }
            },
            async getUserRegistration(userRegId) {
                try {
                    const response = await fetch(`https://localhost:7218/api/Registration/${userRegId}`);

                    if (!response.ok) {
                        throw new Error(`HTTP error! status: ${response.status}`);
                    }

                    if (response.status !== 204) { // 204 is No Content status
                        const data = await response.json();
                        this.user[userRegId] = data;
                        console.log(data);
                        return data;
                    }
                }
                catch (error) {
                    console.error(error);
                }
            }
        },
    });
</script>
<style>
    .card-container {
        display: flex;
        flex-wrap: wrap;
        justify-content: space-between;
    }
    .card {
        background-color: white;
        flex-basis: calc(25% - 20px); /* 4 cards per row, with 10px for margin */
        margin: 5px;
        padding: 20px;
        box-shadow: 0 2px 5px rgba(0, 0, 0, 0.15);
        transition: transform 0.3s ease;
        border-radius: 10px; /* Adjust as needed */
        color: black;
    }
        .card:hover {
            transform: scale(1.05);
        }
        .host{
            font-family: 'Roboto', sans-serif;
            font-size: 0.7em;
            color: darkgray;
        }
</style>
