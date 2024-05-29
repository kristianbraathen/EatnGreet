<template>
    <div id="map" style="height: 100%; width: 100%;"></div>
</template>

<script>
    import { onMounted, ref } from 'vue';
    import L from 'leaflet';
    import 'leaflet/dist/leaflet.css';


    export default {
        setup() {
            const map = ref(null);

            onMounted(() => {
                if (navigator.geolocation) {
                    navigator.geolocation.getCurrentPosition(function (position) {
                        var lat = position.coords.latitude;
                        var lng = position.coords.longitude;
                        map.value = L.map('map').setView([lat, lng], 13);
                        L.tileLayer('https://api.maptiler.com/maps/streets/{z}/{x}/{y}.png?key=ZXTHVIvwMVfIKWBBEsTW', {
                            attribution: '&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
                        }).addTo(map.value);
                    });
                } else {
                    console.log("Geolocation is not supported by this browser.");
                }
            });

            return { map };
        }
    }
</script>

<style scoped>
    #map {
        height: 100%;
    }
</style>
